using UnityEngine;

public class BucleFor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i<= 5; i++){
                Debug.Log("Revisando vagon número : " + i);
        }

        for(int velocidad=0; velocidad<100; velocidad+=20){


            Debug.Log("Velocidad del tren : " + velocidad + "km/h");

        }

        for(int freno =3; freno >= 0; freno-- ){
                        Debug.Log("Liberando presión de freno...");

        }


        for(int i = i; i<4; i++){
            Debug.Log("puerta de estación..." + i + "comprobada");
        }

        for(int i = i; i<=2; i++){
            Debug.Log("Faro del tren " + i + " endendido");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
