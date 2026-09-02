using UnityEngine;
using UnityEngine.UI;


public class CartelHover : MonoBehaviour
{
    // public Image fondo;
    // public Color colorNormal = Color.white;
    // public Color colorSeleccionado = Color.yellow;

    public void Entrar()
    {
        // fondo.color = colorSeleccionado;
        transform.localScale = Vector3.one * 1.1f;
    }

    public void Salir()
    {
        // fondo.color = colorNormal;
        transform.localScale = Vector3.one;
    }
}