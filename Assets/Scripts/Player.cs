using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float factorGravedad;
    [SerializeField] private float alturaSalto;

    [Header("Detección Suelo")]
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
        //Bloquea el ratón en centro de la pantalla y lo oculta
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoverYRotar();

        AplicarGravedad();

        if (EnSuelo())
        {
            movimientoVertical.y = 0;
            Saltar();
        }
    }

    private void Saltar()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           movimientoVertical.y = Mathf.Sqrt(-2 * factorGravedad * alturaSalto);
        }
    }

    private void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(h, v).normalized;


        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y,0);
        //Si el jugador ha tocado teclas...
        if (input.magnitude > 0)
        {

        //Calculo el ángulo al que tengo que rotarme en función de los inputs y cámara.
            float angulo = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            //Mi movimiento también ha quedado rotado en base al ángulo calculado.
            Vector3 movimiento = Quaternion.Euler(0, angulo, 0) * Vector3.forward;

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);

        }


    }
    private void AplicarGravedad()
    {
        //Mi velocidad Vertical va en aumento a cierto factor por segundo.
        movimientoVertical.y += factorGravedad * Time.deltaTime;
        controller.Move(movimientoVertical * Time.deltaTime);
    }

    private bool EnSuelo()
    {
        //Tirar una esfera de detección en los pies con cierto radio.
        bool resultado = Physics.CheckSphere(pies.transform.position, radioDeteccion, queEsSuelo );

        return resultado;
    }
    //metodo que se ejecuta automaticamente para dibujar cualquier figura.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pies.position, radioDeteccion);
    }
}
