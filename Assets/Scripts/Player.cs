using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float factorGraverdad;

    [Header("Detecci�n Suelo")]
    [SerializeField] private float radioDeteccion;
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSuelo;

    private CharacterController controller;

    //Me sirve tanto para la gravedad como para los saltos.
    private Vector3 movimientoVertical;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverYRotar();
        AplicarGravedad();
        EnSuelo();
    }

    private void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, v).normalized;
        //Calculo el �ngulo al que tengo que rotarme en funci�n de los inputs y c�mara.
        float angulo = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        transform.eulerAngles = new Vector3(0, angulo, 0);

        //Si el jugador ha tocado teclas...
        if (input.magnitude > 0)
        {

            //Mi movimiento tambi�n ha quedado rotado en base al �ngulo calculado.
            Vector3 movimiento = Quaternion.Euler(0, angulo, 0) * Vector3.forward;

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);

        }
    }
    private void AplicarGravedad()
    {
        //Mi velocidad Vertical va en aumento a cierto factor por segundo.
        movimientoVertical.y += factorGraverdad * Time.deltaTime;
        controller.Move(movimientoVertical * Time.deltaTime);
    }

    private bool EnSuelo()
    {
        //Tirar una esfera de detecci�n en los pies con cierto radio.
        bool resultado = Physics.CheckSphere(pies.transform.position, radioDeteccion, queEsSuelo );

        return resultado;
    }
}
