using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ControlFinDeNivel1 : MonoBehaviour
{
    public TextMeshProUGUI mensajeFinalTexto;
    private ControlDatosJuego controlDatosJuego;
    void Start()
    {
        controlDatosJuego = GameObject.Find("DatosDeJuego").GetComponent<ControlDatosJuego>();
        string mensajeFinal = (controlDatosJuego.Ganador) ? "Has Ganado" : "Has perdido";
        if (controlDatosJuego.Ganador)
        {
            mensajeFinal += "Puntuacion" + controlDatosJuego.Puntuacion;
        }
        mensajeFinalTexto.text = mensajeFinal;
    }
 
}
