// using UnityEngine;
// using UnityEngine.InputSystem;

// public class ControlSimuladorTren : MonoBehaviour
// {


//     //Si la condición de seguridad se cumple arrancamos.

//     //Variables
//     public int Ntotalvagones = 5;
//     public bool puertacerrada = false;
//     public float VelocidadActual = 0f;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         Debug.Log("BIENVENIDOS AL TREN ARF MADRID");

//         //Agregamos un FOR para verificar los vagones al iniciar el programa
//         for (int i = 0; i <= Ntotalvagones; i++){

//                 Debug.Log("Inspeccionando vagon " + i + "... OK");

//         }

//         Debug.Log("Vagones inspeccionados procede a cerrar las puertas ()");

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         //Condición
//         if(Keyboard.current.spaceKey.wasPressedThisFrame)
//         {
//             ArrancarTren();
//         }

//         if(Keyboard.current.fKey.wasPressedThisFrame)
//         {
//             FrenarTren();
//         }
//     }

//     private void ArrancarTren(){

//         if(puertacerrada){

//             Debug.Log("Puertas seguras");

//             //bucle while
//             while(VelocidadActual<80f){

//                 VelocidadActual+= 20f;
//                 Debug.Log("LA VELOCIDAD " +  "km/h");


//             }

//         }

//         else {
//             Debug.Log("OJO: LAS PUERTAS ESTÁN ABIERTAS - MARCA LA OPCIÓN DESDE EL INSPECTOR. ");
//         }

//     }


//     private void FrenarTren(){
//             Debug.Log("Frenando...");
//             while(VelocidadActual>0f)
//             {
//                     velocidadActual -= 20;
//             }
//                 }

// }
