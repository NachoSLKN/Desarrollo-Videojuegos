using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonPuertaDB : MonoBehaviour
{

    
    public Animator anim;
    private bool isOpen = false;   
    

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
            anim.Play("AnimacionPuertaDeslizanteAbrir");
            isOpen= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        if (other.tag == "Mano" && isOpen == true)
        {
            anim.Play("AnimacionPuertaDeslizanteCerrar");
            isOpen = false;

        }
    }

}
