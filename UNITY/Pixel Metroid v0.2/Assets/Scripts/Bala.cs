using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float velocidad, daño;
    private Animator animacion;
    public bool Izquierda = true;  


    private void Start()

    {

        animacion = GetComponent<Animator>();
        


    }

    // Update is called once per frame
    private void Update()
    {
        if (Izquierda)
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);

        }
        else
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);

        }

        Destroy(gameObject, 2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")

        {       
            collision.GetComponent<ControlEnemigoCangrejo>().TomarDaño(daño);
            animacion.Play("IMPACTOBALA");

            Destroy(gameObject);
        }
    }
}
