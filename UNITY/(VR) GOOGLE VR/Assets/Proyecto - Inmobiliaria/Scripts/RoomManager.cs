using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [Header("Rooms")]
    public GameObject[] rooms; //Array de GameObjects que representan las habiatciones de la casa.
    int lastRoom; 



   private void Start()
    {

        foreach (GameObject room in rooms){
            room.SetActive(false);
        }

        ChangeRoom(0);
    }


    public void ChangeRoom(int ID){

        rooms[lastRoom].SetActive(false); //Desactivamos la última habitación activada.
        rooms[ID].SetActive(true); //Se activa la nueva habitación.

        lastRoom = ID;


    }

    
}
