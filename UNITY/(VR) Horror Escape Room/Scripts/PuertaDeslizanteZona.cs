using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaDeslizanteZona : MonoBehaviour
{
    public Animator anim;
    private bool isOpen = false;

    private bool onCooldown = false;
    private float Cooldown = 0;

    private bool locked = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() //Miramos si estamos haciendo la cuenta atrás de los 7 segundos y suma si es así. 
        //Si en el frame en el que estamos la suma pasa de 7 se resetea y DIOS MIO PARA. 
    {

        if (onCooldown)
        {
            Cooldown += Time.deltaTime;
            if (Cooldown > 7)
            {
                onCooldown = false;
                Cooldown = 0;

                anim.Play("AnimacionPuertaDeslizanteCerrar");
                locked = true;

            }
        }



    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player" && isOpen == false && !locked)
        {
            Debug.Log("vamo");
            anim.Play("AnimacionPuertaDeslizanteAbrir");
            isOpen = true;
            //Empezar cuenta regresiva de 7 segundos
            onCooldown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {


        if (other.tag == "Player" && isOpen == true)
        {
            anim.Play("AnimacionPuertaDeslizanteCerrar");
            isOpen = false;

        }
    }

}
