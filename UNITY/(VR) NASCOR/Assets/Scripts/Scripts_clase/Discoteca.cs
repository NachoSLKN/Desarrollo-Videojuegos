using UnityEngine;

public class Discoteca : MonoBehaviour
{

    public int edad = 20;
    string nombre = "Nacho";




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {




        if (edad < 18)
        {
            Debug.Log("Eres menor de edad");
        }

        else if (edad >= 18)
        {
            Debug.Log("Eres mayor de edad");
        }

        else
        {
            Debug.Log("Cualquier edad");
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
