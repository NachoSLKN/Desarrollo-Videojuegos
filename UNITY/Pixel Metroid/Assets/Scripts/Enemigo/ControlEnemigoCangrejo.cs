using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigoCangrejo : MonoBehaviour
{
    public float velocidad;
    public Vector3 posicionFin;

    private Vector3 posicionInicio;
    private bool direccionDestino;

    public float vida;

    void Start()
    {
        posicionInicio = transform.position;
        direccionDestino = true;
    }

    
    void Update()
    {
        DesplazarEnemigo();
    }

    private void DesplazarEnemigo()
    {
        Vector3 posicionDestino=direccionDestino ? posicionFin : posicionInicio;
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad*Time.deltaTime);
        if(transform.position == posicionFin)
        {
            direccionDestino=false;
        }

        if(transform.position == posicionInicio)
        {
            direccionDestino = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControlJugador>().QuitarVida(); 

        }
    }

    public void TomarDaño(float daño)
    {

        vida -= daño;
        //vida = vida - daño;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }

    }
}
