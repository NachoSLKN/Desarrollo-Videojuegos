using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class IronGiantMovement : MonoBehaviour
{
    [Header("Ground Movement")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float rotationSpeed = 8f;

    [Header("Flying")]
    [SerializeField] private float flySpeed = 8f;
    [SerializeField] private float verticalFlySpeed = 6f;

    [Header("Flight Effects")]
    [SerializeField] private ParticleSystem leftFootFire;
    [SerializeField] private ParticleSystem leftFootSmoke;
    [SerializeField] private ParticleSystem rightFootFire;
    [SerializeField] private ParticleSystem rightFootSmoke;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float groundedForce = -2f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.35f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float animationDampTime = 0.15f;

    [Header("Debug")]
    [SerializeField] private bool showDebug;

    private CharacterController characterController;

    private Vector2 moveInput;
    private float flyVerticalInput;
    private float verticalVelocity;

    private bool isSprinting;
    private bool isPushing;
    private bool isGrounded;
    private bool isFlying;

    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private static readonly int IsPushingHash =
        Animator.StringToHash("IsPushing");

    private static readonly int IsGroundedHash =
        Animator.StringToHash("IsGrounded");

    private static readonly int IsFlyingHash =
        Animator.StringToHash("IsFlying");

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError(
                "No Animator was found on IronGiant_Player or its children.",
                this
            );
        }

        if (groundCheck == null)
        {
            Debug.LogError(
                "GroundCheck has not been assigned.",
                this
            );
        }

        /*
         * Nos aseguramos de que los propulsores estén completamente
         * apagados al comenzar la escena.
         */
        StopFlightEffects(true);
    }

    private void Update()
    {
        UpdateSprintInput();
        CheckGround();

        if (isFlying)
        {
            MoveFlying();
        }
        else
        {
            MoveOnGround();
        }

        UpdateAnimator();
    }

    // Input: movement with keyboard or left stick.
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.sqrMagnitude < 0.01f)
        {
            moveInput = Vector2.zero;
        }
        else
        {
            moveInput = Vector2.ClampMagnitude(moveInput, 1f);
        }

        if (showDebug)
        {
            Debug.Log($"[MOVE] {moveInput}", this);
        }
    }

    // Input: hold Shift / gamepad button to sprint.
    //public void OnSprint(InputValue value)
    //{
    //    isSprinting = value.isPressed;

    //    if (showDebug)
    //    {
    //        Debug.Log($"[SPRINT] {isSprinting}", this);
    //    }
    //}

    // Input: E toggles pushing mode.
    public void OnInteract(InputValue value)
    {
        if (!value.isPressed || isFlying)
        {
            return;
        }

        isPushing = !isPushing;

        if (animator != null)
        {
            animator.SetBool(
                IsPushingHash,
                isPushing
            );
        }
    }

    // Input: F / gamepad button toggles flying mode.
    public void OnFly(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        isFlying = !isFlying;

        if (isFlying)
        {
            verticalVelocity = 0f;
            isPushing = false;

            PlayFlightEffects();
        }
        else
        {
            /*
             * Dejamos de emitir, pero no borramos las partículas
             * existentes. El humo seguirá flotando hasta desaparecer.
             */
            StopFlightEffects(false);
        }

        if (animator != null)
        {
            animator.SetBool(
                IsFlyingHash,
                isFlying
            );

            animator.SetBool(
                IsPushingHash,
                false
            );
        }

        if (showDebug)
        {
            Debug.Log($"[FLY] {isFlying}", this);
        }
    }

    // Input: used to go up and down while flying.
    public void OnFlyVertical(InputValue value)
    {
        flyVerticalInput = value.Get<float>();

        if (Mathf.Abs(flyVerticalInput) < 0.01f)
        {
            flyVerticalInput = 0f;
        }
    }

    private void CheckGround()
    {
        if (groundCheck == null)
        {
            isGrounded =
                characterController.isGrounded;
        }
        else
        {
            isGrounded = Physics.CheckSphere(
                groundCheck.position,
                groundCheckRadius,
                groundLayer,
                QueryTriggerInteraction.Ignore
            );
        }

        if (animator != null)
        {
            animator.SetBool(
                IsGroundedHash,
                isGrounded
            );
        }

        if (
            isGrounded &&
            !isFlying &&
            verticalVelocity < 0f
        )
        {
            verticalVelocity = groundedForce;
        }
    }

    private void MoveOnGround()
    {
        Vector3 direction = new Vector3(
            moveInput.x,
            0f,
            moveInput.y
        );

        bool isMoving =
            direction.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            direction.Normalize();
            RotateTowards(direction);
        }

        if (!isGrounded)
        {
            verticalVelocity +=
                gravity * Time.deltaTime;
        }

        float horizontalSpeed = 0f;

        if (isMoving)
        {
            horizontalSpeed = isSprinting
                ? runSpeed
                : walkSpeed;
        }

        Vector3 velocity =
            direction * horizontalSpeed +
            Vector3.up * verticalVelocity;

        characterController.Move(
            velocity * Time.deltaTime
        );
    }

    private void MoveFlying()
    {
        Vector3 horizontalDirection =
            new Vector3(
                moveInput.x,
                0f,
                moveInput.y
            );

        if (horizontalDirection.sqrMagnitude > 1f)
        {
            horizontalDirection.Normalize();
        }

        if (horizontalDirection.sqrMagnitude > 0.01f)
        {
            RotateTowards(
                horizontalDirection
            );
        }

        Vector3 horizontalVelocity =
            horizontalDirection * flySpeed;

        Vector3 verticalVelocityVector =
            Vector3.up *
            flyVerticalInput *
            verticalFlySpeed;

        Vector3 finalVelocity =
            horizontalVelocity +
            verticalVelocityVector;

        characterController.Move(
            finalVelocity * Time.deltaTime
        );
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation =
            Quaternion.LookRotation(
                direction,
                Vector3.up
            );

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
    }

    private void PlayFlightEffects()
    {
        PlayParticleSystem(leftFootFire);
        PlayParticleSystem(leftFootSmoke);
        PlayParticleSystem(rightFootFire);
        PlayParticleSystem(rightFootSmoke);

        if (showDebug)
        {
            Debug.Log(
                "[FLIGHT FX] Thrusters started.",
                this
            );
        }
    }

    private void StopFlightEffects(
        bool clearExistingParticles
    )
    {
        ParticleSystemStopBehavior stopBehavior =
            clearExistingParticles
                ? ParticleSystemStopBehavior
                    .StopEmittingAndClear
                : ParticleSystemStopBehavior
                    .StopEmitting;

        StopParticleSystem(
            leftFootFire,
            stopBehavior
        );

        StopParticleSystem(
            leftFootSmoke,
            stopBehavior
        );

        StopParticleSystem(
            rightFootFire,
            stopBehavior
        );

        StopParticleSystem(
            rightFootSmoke,
            stopBehavior
        );

        if (showDebug)
        {
            Debug.Log(
                clearExistingParticles
                    ? "[FLIGHT FX] Thrusters stopped and cleared."
                    : "[FLIGHT FX] Thrusters stopped emitting.",
                this
            );
        }
    }

    private static void PlayParticleSystem(
        ParticleSystem particleSystem
    )
    {
        if (particleSystem == null)
        {
            return;
        }

        if (!particleSystem.isPlaying)
        {
            particleSystem.Play(
                true
            );
        }
    }

    private static void StopParticleSystem(
        ParticleSystem particleSystem,
        ParticleSystemStopBehavior stopBehavior
    )
    {
        if (particleSystem == null)
        {
            return;
        }

        particleSystem.Stop(
            true,
            stopBehavior
        );
    }

    private void UpdateAnimator()
    {
        if (animator == null)
        {
            return;
        }

        bool isMoving =
            moveInput.sqrMagnitude > 0.01f;

        float targetSpeed;

        if (isFlying || !isMoving)
        {
            targetSpeed = 0f;
        }
        else if (isSprinting)
        {
            targetSpeed = 1f;
        }
        else
        {
            targetSpeed = 0.5f;
        }

        if (targetSpeed <= 0f)
        {
            animator.SetFloat(
                SpeedHash,
                0f
            );
        }
        else
        {
            animator.SetFloat(
                SpeedHash,
                targetSpeed,
                animationDampTime,
                Time.deltaTime
            );
        }

        animator.SetBool(
            IsPushingHash,
            isPushing
        );

        animator.SetBool(
            IsGroundedHash,
            isGrounded
        );

        animator.SetBool(
            IsFlyingHash,
            isFlying
        );
    }

    private void ResetInputState()
    {
        moveInput = Vector2.zero;
        flyVerticalInput = 0f;

        isSprinting = false;
        isPushing = false;
        isFlying = false;

        verticalVelocity = 0f;

        /*
         * Aquí sí limpiamos todo porque el componente
         * está perdiendo el foco o desactivándose.
         */
        StopFlightEffects(true);

        if (animator != null)
        {
            animator.SetFloat(
                SpeedHash,
                0f
            );

            animator.SetBool(
                IsPushingHash,
                false
            );

            animator.SetBool(
                IsFlyingHash,
                false
            );
        }
    }

    private void OnDisable()
    {
        ResetInputState();
    }

    private void OnApplicationFocus(
        bool hasFocus
    )
    {
        if (!hasFocus)
        {
            ResetInputState();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }

    private void UpdateSprintInput()
    {
        bool keyboardSprint =
            Keyboard.current != null &&
            Keyboard.current.leftShiftKey.isPressed;

        bool gamepadSprint =
            Gamepad.current != null &&
            Gamepad.current.leftStickButton.isPressed;

        isSprinting = keyboardSprint || gamepadSprint;
    }

}