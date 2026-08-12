using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
    public float speed = 3f;
    public CharacterController controller;
    public Fighter fighter;

    private Vector3 position;
    private Animator anim;
    private Camera mainCamera;

    public GameObject inventoryPanel;


    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (fighter == null)
            fighter = GetComponent<Fighter>();

        anim = GetComponent<Animator>();
        mainCamera = Camera.main;

        position = transform.position;
    }

    void Update()
    {
        // Si el ratón está sobre la interfaz, no movemos al personaje.
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        // Si el inventario está abierto, tampoco permitimos movernos.
        if (inventoryPanel != null && inventoryPanel.activeSelf)
            return;

        // Detiene el movimiento mientras Don Quijote está atacando.
        if (fighter != null && fighter.IsAttacking)
        {
            controller.SimpleMove(Vector3.zero);

            if (anim != null)
                anim.SetFloat("Speed", 0f);

            return;
        }

        // Nuevo Input System.
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            LocatePosition();
        }

        MoveToPosition();
    }

    void LocatePosition()
    {
        if (mainCamera == null || Mouse.current == null)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            // Al clicar en un enemigo no cambiamos el destino.
            if (hit.collider.CompareTag("Enemy"))
                return;

            // Al clicar en Don Quijote tampoco hacemos nada.
            if (hit.collider.CompareTag("Player"))
                return;

            // Al clicar en el suelo salimos del objetivo anterior.
            if (fighter != null)
                fighter.opponent = null;

            position = hit.point;
        }
    }

    void MoveToPosition()
    {
        if (controller == null)
            return;

        Vector3 direction = position - transform.position;

        // Ignoramos la diferencia vertical para evitar inclinaciones y giros raros.
        direction.y = 0f;

        bool isMoving = direction.magnitude > 0.2f;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * 10f
            );

            controller.SimpleMove(transform.forward * speed);

            if (anim != null)
                anim.SetFloat("Speed", 1f);
        }
        else
        {
            controller.SimpleMove(Vector3.zero);

            if (anim != null)
                anim.SetFloat("Speed", 0f);
        }
    }
}