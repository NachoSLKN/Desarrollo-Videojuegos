using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour
{

    //Cuando Player entra en Agua se asigna mitad de velocidad 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<ControlJugador>().velocidad = 2;
        }
    }

    //Cuando Player sale del trigger del Agua se restablece velocidad 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<ControlJugador>().velocidad = 4;
        }
    }


}
