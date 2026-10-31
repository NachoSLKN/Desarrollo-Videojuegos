using UnityEngine;
using UnityEngine.InputSystem;

// Colocar en el objeto PADRE: METODOS_MOVIMIENTO.
// PlayerInput en el MISMO GameObject: mapa Player, Send Messages.
[RequireComponent(typeof(PlayerInput))]
public class InputComparativaMovimiento : MonoBehaviour
{
    public Vector2 Direccion { get; private set; }

    // La accion "Move" de InputSystem_Actions llama a OnMove.
    public void OnMove(InputValue valor)
    {
        Direccion = Vector2.ClampMagnitude(valor.Get<Vector2>(), 1f);
    }

    private void OnDisable()
    {
        Direccion = Vector2.zero;
    }
}
