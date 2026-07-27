using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Script Ref")]

    InputManager inputManager;

    [Header("Movement")]
    Vector3 moveDirection;

    public Transform camObject;

    Rigidbody playerRigidbody;

    public float movementSpeed = 2f;
    public float rotationSpeed = 12f;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // Seteamos el movimiento usando nuestros inputs,
        // calculamos la dirección en base a la cámara y el input.
        moveDirection = camObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + camObject.right * inputManager.horizontalInput;

        // Normalizamos
        moveDirection.Normalize();
        moveDirection.y = 0;

        // Determinamos el movimiento final
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.angularVelocity = movementVelocity;
    }

    void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = camObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + camObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        transform.rotation = playerRotation;
    }
}