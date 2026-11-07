
using System.Collections;
using UnityEngine;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(XROrigin))]
public class InicioVR : MonoBehaviour
{
    [SerializeField] private Transform puntoInicio;

    private IEnumerator Start()
    {
        // Esperar un frame para recibir la posicion del visor.
        yield return null;

        XROrigin xrOrigin = GetComponent<XROrigin>();

        if (puntoInicio == null || xrOrigin.Camera == null)
        {
            Debug.LogError("Falta asignar PuntoInicioVR o la camara XR.");
            yield break;
        }

        // Mantener la altura real del visor.
        Vector3 destino = puntoInicio.position;
        destino.y = xrOrigin.Camera.transform.position.y;

        // Mover el XR Origin para situar la camara en el destino.
        xrOrigin.MoveCameraToWorldLocation(destino);
    }
}
