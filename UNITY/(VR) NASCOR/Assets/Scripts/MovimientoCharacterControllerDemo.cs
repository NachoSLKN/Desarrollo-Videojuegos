using UnityEngine;

// 05: CharacterController.Move, sin Rigidbody; gravedad manual.
[RequireComponent(typeof(CharacterController))]
public class MovimientoCharacterControllerDemo : MonoBehaviour
{
    [SerializeField, Min(0f)] private float velocidad = 5f;
    [SerializeField] private float gravedad = -9.81f;

    private InputComparativaMovimiento entrada;
    private CharacterController controlador;
    private float velocidadVertical;

    private void Awake()
    {
        entrada = GetComponentInParent<InputComparativaMovimiento>();
        controlador = GetComponent<CharacterController>();
        if (entrada == null) Debug.LogError("Falta InputComparativaMovimiento en el padre.", this);
    }

    private void Update()
    {
        Vector2 ejes = entrada != null ? entrada.Direccion : Vector2.zero;
        Vector3 horizontal = new Vector3(ejes.x, 0f, ejes.y) * velocidad;

        if (controlador.isGrounded && velocidadVertical < 0f)
            velocidadVertical = -2f;
        velocidadVertical += gravedad * Time.deltaTime;

        controlador.Move((horizontal + Vector3.up * velocidadVertical) * Time.deltaTime);
    }
}
