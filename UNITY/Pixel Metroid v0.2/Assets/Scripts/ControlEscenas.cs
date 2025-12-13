using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlEscenas : MonoBehaviour
{
   public void OnBotonJugar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void OnBotonMenu()
    {
        SceneManager.LoadScene("MenuDeInicio");
    }

    public void OnBotonSalir()
    {
        Application.Quit(); 
    }

    public void OnBotonCreditos()
    {

        SceneManager.LoadScene("Creditos");
    }
}
