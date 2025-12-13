using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System; 

public class ScriptControlMando : MonoBehaviour
{



    MapeadoJugador controles; 

    private void Awake()

    {

        controles = new MapeadoJugador();

    }

    private void OnEnable()

    {
        controles.Jugador.Enable();

        //Boton de ida y vuelta / Empieza - Acaba 
        


        controles.Jugador.Saltar.performed += MovePlayer;
        //Cancelled//Performed//Started 
        //Boton de un solo uso 
        //Performed he pulsado / Cancelled he soltado 
        controles.Jugador.Agacharse.started += MovePlayer;
        controles.Jugador.Agacharse.canceled += MovePlayer;

        controles.Jugador.Saltar.performed += MovePlayer; 

    }

    private void MovePlayer(InputAction.CallbackContext obj)
    {
        Vector2 DirecMov = obj.ReadValue<Vector2>();
        FindObjectOfType<ControlJugador>().Correr(DirecMov);
    }

}
