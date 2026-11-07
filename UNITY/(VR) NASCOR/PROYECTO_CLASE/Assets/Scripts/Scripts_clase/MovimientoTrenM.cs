
using UnityEngine;
using UnityEngine.InputSystem; //----> Interacción.

public class MovimientoTren : MonoBehaviour
{

    public float velocidad = 10f;
    private bool moviendoAdelante; //Sí se ha pulsado la tecla.
    private bool moviendoAtras; 

    //Movimiento del tren de forma manual. 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(moviendoAdelante){
        Debug.Log("Avanzando...");
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }


        if(moviendoAtras){
        Debug.Log("Retrocediendo...");
        transform.Translate(Vector3.back * velocidad * Time.deltaTime);
        }

        // Avanzar : si presionamos W en el teclado.
        // if(Keyboard.current.wKey.isPressed || Gamepad.current.leftStick.y.ReadValue()>0.1f){
        // Debug.Log("Estás pulsando la tecla W");
        // transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        // };


        // Retroceder : si presionamos W en el teclado.
        // if(Keyboard.current.sKey.isPressed || Gamepad.current.leftStick.y.ReadValue()<-0.1f){
        // Debug.Log("Estás pulsando la tecla S");
        // transform.Translate(Vector3.back * velocidad * Time.deltaTime);
        // };

        //Automatización
        //transform.Translate(Vector3.back * velocidad * Time.deltaTime);


    }


    public void OnAdelante(InputValue valor){


        // if(valor.isPressed){
        // transform.Translate(Vector3.back * velocidad * Time.deltaTime);

        // }
        moviendoAdelante = valor.isPressed;


    }

    public void OnAtras(InputValue valor){
        moviendoAtras = valor.isPressed;
    }
}
