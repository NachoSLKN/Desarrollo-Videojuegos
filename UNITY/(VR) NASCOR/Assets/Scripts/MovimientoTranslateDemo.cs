using UnityEngine;

// 01: mueve el Transform en sus ejes LOCALES, sin resolver colisiones.
[RequireComponent(typeof(CapsuleCollider))]
public class MovimientoTranslateDemo : MonoBehaviour
{
    [SerializeField, Min(0f)] private float velocidad = 5f;
    private InputComparativaMovimiento entrada;

    private void Awake()
    {
        entrada = GetComponentInParent<InputComparativaMovimiento>();
        if (entrada == null) Debug.LogError("Falta InputComparativaMovimiento en el padre.", this);
    }

    private void Update()
    {
        if (entrada == null) return;
        Vector2 ejes = entrada.Direccion;
        Vector3 direccion = new Vector3(ejes.x, 0f, ejes.y);
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.Self);
    }
}
