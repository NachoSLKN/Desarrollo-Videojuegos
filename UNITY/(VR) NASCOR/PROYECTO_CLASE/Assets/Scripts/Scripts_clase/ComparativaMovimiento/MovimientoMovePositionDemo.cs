using UnityEngine;

// 04: Rigidbody CINEMATICO controlado por MovePosition en pasos fisicos.
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class MovimientoMovePositionDemo : MonoBehaviour
{
    [SerializeField, Min(0f)] private float velocidad = 5f;
    private InputComparativaMovimiento entrada;
    private Rigidbody cuerpo;

    private void Awake()
    {
        entrada = GetComponentInParent<InputComparativaMovimiento>();
        cuerpo = GetComponent<Rigidbody>();
        if (entrada == null) Debug.LogError("Falta InputComparativaMovimiento en el padre.", this);
        if (!cuerpo.isKinematic) Debug.LogWarning("04_MovePosition debe tener Is Kinematic ACTIVADO.", this);
    }

    private void FixedUpdate()
    {
        if (entrada == null) return;
        Vector2 ejes = entrada.Direccion;
        Vector3 direccion = new Vector3(ejes.x, 0f, ejes.y);
        Vector3 destino = cuerpo.position + direccion * velocidad * Time.fixedDeltaTime;
        cuerpo.MovePosition(destino);
    }
}
