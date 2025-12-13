using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    public int cantidad;
    public AudioClip powerAudio;
    public int velocidad;

    private GameObject PowerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  
        
        {
            collision.gameObject.GetComponent<ControlJugador>().IncrementarPuntos(cantidad);
            collision.GetComponent<AudioSource>().PlayOneShot(powerAudio);
            Destroy(gameObject);
        }

    }

   
}
