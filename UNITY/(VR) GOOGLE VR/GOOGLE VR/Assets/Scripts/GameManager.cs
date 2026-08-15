using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image imgFade;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("Fade", 1f); //Usamos Invoke para llamar a la función Fade después de 1 segundo


    }

    private void Fade()
    {
        imgFade.CrossFadeAlpha(0f, 2f, true); // Alfa, duration, ignoreTimeScale

    }


}