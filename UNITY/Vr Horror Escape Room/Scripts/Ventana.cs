using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventana : MonoBehaviour
{
    public Animator anim;

    public Animator Fantasma;

    private bool isOpen = false;

    public bool isGhostWindow = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Mano" && isOpen == false)
        {
            Debug.Log("vamo");
            anim.Play("VentanaAbierta");
            isOpen = true;

            if (isGhostWindow)
            {
                Fantasma.Play("FantasmaIdleBucle");

            }


        }
    }

}
