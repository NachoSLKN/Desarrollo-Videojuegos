using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerMovement playerMovement;
    private CameraManager cameraManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        cameraManager = FindFirstObjectByType<CameraManager>();
    }

    private void Update()
    {
        // Input y parámetros del Animator.
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        // Todo lo relacionado con el Rigidbody.
        playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {
        // La cámara se actualiza después de mover al personaje.
        cameraManager.HandleAllCameraMovement();
    }
}