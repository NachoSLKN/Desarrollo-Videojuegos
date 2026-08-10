using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private AnimatorManager animatorManager;
    private PlayerMovement playerMovement;

    public Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;
    public float cameraInputX;
    public float cameraInputY;

    private Vector2 cameraInput;

    [Header("Input Button Flags")]
    public bool bInput;
    public bool shootInput;
    public bool scopeInput;
    public bool reloadInput;
    public bool pauseInput;
    public bool changeRifleInput;

    public bool EInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Movimiento
        playerControls.PlayerMovement.Movement.performed +=
            context => movementInput = context.ReadValue<Vector2>();

        playerControls.PlayerMovement.Movement.canceled +=
            context => movementInput = Vector2.zero;

        // Cámara
        playerControls.PlayerMovement.CameraMovement.performed +=
            context => cameraInput = context.ReadValue<Vector2>();

        playerControls.PlayerMovement.CameraMovement.canceled +=
            context => cameraInput = Vector2.zero;

        // Sprint
        playerControls.PlayerActions.B.performed +=
            context => bInput = true;

        playerControls.PlayerActions.B.canceled +=
            context => bInput = false;

        // Disparo
        playerControls.PlayerActions.Shoot.performed +=
            context => shootInput = true;

        playerControls.PlayerActions.Shoot.canceled +=
            context => shootInput = false;

        // Apuntar
        playerControls.PlayerActions.Scope.performed +=
            context => scopeInput = true;

        playerControls.PlayerActions.Scope.canceled +=
            context => scopeInput = false;

        // Recargar
        playerControls.PlayerActions.Reload.performed +=
            context => reloadInput = true;

        playerControls.PlayerActions.Reload.canceled +=
            context => reloadInput = false;

        // Pausa
        playerControls.PlayerActions.Pause.performed +=
            context => pauseInput = true;

        playerControls.PlayerActions.Pause.canceled +=
            context => pauseInput = false;

        // Cambiar arma: una pulsación, no mantener
        playerControls.PlayerActions.C.performed +=
            context => changeRifleInput = true;

        // E
        playerControls.PlayerActions.E.performed +=
            context => EInput = true;




        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleEInput();

        animatorManager.UpdateAnimValues(
            horizontalInput,
            verticalInput,
            playerMovement.isRunning
        );
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(
            Mathf.Abs(horizontalInput) +
            Mathf.Abs(verticalInput)
        );
    }

    private void HandleSprintingInput()
    {
        playerMovement.isRunning =
            bInput && moveAmount > 0.5f;
    }

    IEnumerator HandleEInput()
    {
        yield return new WaitForSeconds(.2f);
        if (EInput)
        {
            EInput = false;
        }
    }



}