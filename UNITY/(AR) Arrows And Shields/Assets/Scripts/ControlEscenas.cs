using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscenas : MonoBehaviour
{
        public void OnBotonJugar()
        {
            SceneManager.LoadScene("GAMESCENE");
        }

        public void OnBotonMenu()
        {
            SceneManager.LoadScene("MAINMENU");
        }

        public void OnBotonSalir()
        {
            Application.Quit();
        }

        public void OnBotonCreditos()
        {

            SceneManager.LoadScene("Creditos");
        }

        public void OnBotonCómoJugar()
       {

        SceneManager.LoadScene("CÓMOJUGAR");

        }

}
