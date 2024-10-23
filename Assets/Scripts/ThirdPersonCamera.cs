using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float smoothTime;
    private CharacterController controller;

    private float velocidadRotacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoverYRotar();
    }
    private void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, v).normalized;
        //Calculo el �ngulo al que tengo que rotarme en funci�n de los inputs y c�mara.

       

        //Si el jugador ha tocado teclas...
        if (input.magnitude > 0)
        {
            float angulo = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float anguloSuave = Mathf.SmoothDampAngle(transform.eulerAngles.y, angulo, ref velocidadRotacion, smoothTime);

            transform.eulerAngles = new Vector3(0,anguloSuave, 0);
            //Mi movimiento tambi�n ha quedado rotado en base al �ngulo calculado.
            Vector3 movimiento = Quaternion.Euler(0, angulo, 0) * Vector3.forward;

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);

        }
    }
}
