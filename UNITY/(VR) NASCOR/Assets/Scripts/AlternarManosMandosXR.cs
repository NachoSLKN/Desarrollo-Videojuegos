using UnityEngine;
using UnityEngine.InputSystem;

// Colocar en XR Origin (XR Rig), NO en cada mano.
// Alterna únicamente modelos visuales: no desactiva controladores ni interactores XR.
public class AlternarManosMandosXR : MonoBehaviour
{
    [Header("Modelos de manos")]
    [SerializeField] private GameObject manoIzquierda;
    [SerializeField] private GameObject manoDerecha;

    [Header("Modelos de mandos")]
    [SerializeField] private GameObject mandoIzquierdo;
    [SerializeField] private GameObject mandoDerecho;

    [Header("Estado inicial")]
    [SerializeField] private bool empezarConManos = true;

    private bool mostrandoManos;
    private InputAction cambiarVista;

    private void Awake()
    {
        mostrandoManos = empezarConManos;
        AplicarVista();

        cambiarVista = new InputAction("Cambiar manos/mandos", InputActionType.Button);
        cambiarVista.AddBinding("<Keyboard>/v");
        cambiarVista.AddBinding("<OculusTouchController>{RightHand}/primaryButton"); // Boton A (Quest)
    }

    private void OnEnable()
    {
        cambiarVista.performed += AlPulsarCambio;
        cambiarVista.Enable();
    }

    private void OnDisable()
    {
        cambiarVista.performed -= AlPulsarCambio;
        cambiarVista.Disable();
    }

    private void OnDestroy()
    {
        cambiarVista?.Dispose();
    }

    private void AlPulsarCambio(InputAction.CallbackContext contexto)
    {
        Alternar();
    }

    // También se puede vincular a un botón UI en su evento OnClick.
    public void Alternar()
    {
        mostrandoManos = !mostrandoManos;
        AplicarVista();
    }

    private void AplicarVista()
    {
        if (manoIzquierda != null) manoIzquierda.SetActive(mostrandoManos);
        if (manoDerecha != null) manoDerecha.SetActive(mostrandoManos);
        if (mandoIzquierdo != null) mandoIzquierdo.SetActive(!mostrandoManos);
        if (mandoDerecho != null) mandoDerecho.SetActive(!mostrandoManos);
    }
}
