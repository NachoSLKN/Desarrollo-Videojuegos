using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlHud : MonoBehaviour
{
    public TextMeshProUGUI vidasTxt;
    public TextMeshProUGUI tiempoTxt;
    public TextMeshProUGUI objetosTxt;




    public void SetVidasTXT(int vidas)
    {
        vidasTxt.text="VIDAS:"+ vidas;
    }


    public void SetTiempoTXT(int tiempo)
    {
        int segundos = tiempo % 60;
        int minutos = tiempo / 60;
        tiempoTxt.text =minutos.ToString("00")+ ":"+ segundos.ToString("00");
    }


    public void SetObjetosTXT(int objetos)
    {
        objetosTxt.text = "OBJETOS:" + objetos;
    }
}




