using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; 

public class HMDManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        Debug.Log("Dispositivo Activo: " + XRSettings.isDeviceActive);
        Debug.Log("Nombre del dispotivo: " + XRSettings.loadedDeviceName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
