using UnityEngine;

public class MenuInicialVR : MonoBehaviour
{


    public GameObject panelJugar;
    public GameObject panelObjetivos;
    public GameObject panelControles;
    public GameObject sistemaLocomotion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panelJugar.SetActive(true);
        panelObjetivos.SetActive(false);
        panelControles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotonJugarPulsado(){

    }
}
