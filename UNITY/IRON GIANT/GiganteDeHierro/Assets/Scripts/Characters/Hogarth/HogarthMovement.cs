using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class HogarthMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float groundedForce = -2f;

    [Header("Simple Ledge Detection")]
    [SerializeField] private Transform ledgeWallCheck;
    [SerializeField] private LayerMask ledgeLayer;

    [Tooltip("Radius of the sphere used to detect objects on the Ledge layer.")]
    [SerializeField] private float wallCheckDistance = 0.55f;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpUpForce = 5f;
    [SerializeField] private float wallJumpBackForce = 3f;
    [SerializeField] private float wallJumpControlDelay = 0.25f;

    [Tooltip("Time before Hogarth can grab the same wall again after jumping.")]
    [SerializeField] private float ledgeRegrabDelay = 0.5f;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float animationDampTime = 0.12f;

    [Header("Debug")]
    [SerializeField] private bool showInputDebug = true;
    [SerializeField] private bool drawLedgeDebug = true;

    private CharacterController characterController;

    private Vector2 moveInput;
    private float verticalVelocity;

    private bool isHanging;
    private bool isJumpingFromWall;

    private Vector3 wallJumpHorizontalVelocity;
    private float wallJumpTimer;
    private float ledgeRegrabTimer;

    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private static readonly int IsGroundedHash =
        Animator.StringToHash("IsGrounded");

    private static readonly int IsHangingHash =
        Animator.StringToHash("IsHanging");

    private static readonly int HangDirectionHash =
        Animator.StringToHash("HangDirection");

    private static readonly int JumpFromWallHash =
        Animator.StringToHash("JumpFromWall");

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
                "No Animator was found on Hogarth_Player or its children.",
                this
            );
        }

        if (ledgeWallCheck == null)
        {
            Debug.LogWarning(
                "LedgeWallCheck has not been assigned.",
                this
            );
        }
    }

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

        if (showInputDebug)
        {
            Debug.Log(
                $"[HOGARTH MOVE] Input: {moveInput}",
                this
            );
        }
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        if (showInputDebug)
        {
            Debug.Log("[HOGARTH] Jump input received.", this);
        }

        if (isHanging)
        {
            JumpFromWall();
        }
    }

    private void Update()
    {
        UpdateTimers();

        if (isHanging)
        {
            UpdateHanging();
            UpdateAnimator();
            return;
        }

        CheckForLedge();
        MoveCharacter();
        UpdateAnimator();
    }

    private void UpdateTimers()
    {
        if (ledgeRegrabTimer > 0f)
        {
            ledgeRegrabTimer -= Time.deltaTime;
        }
    }

    private void MoveCharacter()
    {
        Vector3 direction = new Vector3(
            moveInput.x,
            0f,
            moveInput.y
        );

        float inputMagnitude =
            Mathf.Clamp01(direction.magnitude);

        bool canUseMovementInput =
            !isJumpingFromWall ||
            wallJumpTimer <= 0f;

        if (!canUseMovementInput)
        {
            direction = Vector3.zero;
            inputMagnitude = 0f;
        }

        bool isMoving = inputMagnitude > 0.01f;

        if (isMoving)
        {
            direction.Normalize();

            Quaternion targetRotation =
                Quaternion.LookRotation(
                    direction,
                    Vector3.up
                );

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        UpdateGravity();

        Vector3 horizontalVelocity;

        if (isJumpingFromWall && wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
            horizontalVelocity = wallJumpHorizontalVelocity;
        }
        else
        {
            isJumpingFromWall = false;

            horizontalVelocity =
                direction * walkSpeed * inputMagnitude;
        }

        Vector3 velocity =
            horizontalVelocity +
            Vector3.up * verticalVelocity;

        characterController.Move(
            velocity * Time.deltaTime
        );
    }

    private void UpdateGravity()
    {
        if (
            characterController.isGrounded &&
            verticalVelocity < 0f
        )
        {
            verticalVelocity = groundedForce;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void CheckForLedge()
    {
        if (isHanging)
        {
            return;
        }

        if (isJumpingFromWall)
        {
            return;
        }

        if (ledgeRegrabTimer > 0f)
        {
            return;
        }

        if (ledgeWallCheck == null)
        {
            return;
        }

        bool touchedLedge = Physics.CheckSphere(
            ledgeWallCheck.position,
            wallCheckDistance,
            ledgeLayer,
            QueryTriggerInteraction.Ignore
        );

        if (touchedLedge)
        {
            EnterSimpleHangingState();
        }
    }

    private void EnterSimpleHangingState()
    {
        isHanging = true;
        isJumpingFromWall = false;

        verticalVelocity = 0f;
        moveInput = Vector2.zero;

        wallJumpTimer = 0f;
        wallJumpHorizontalVelocity = Vector3.zero;

        if (animator != null)
        {
            animator.SetFloat(SpeedHash, 0f);
            animator.SetFloat(HangDirectionHash, 0f);
            animator.SetBool(IsHangingHash, true);
        }

        if (showInputDebug)
        {
            Debug.Log(
                "[HOGARTH] Simple ledge detected. Hanging activated.",
                this
            );
        }
    }

    private void UpdateHanging()
    {
        verticalVelocity = 0f;

        if (animator != null)
        {
            animator.SetFloat(
                HangDirectionHash,
                moveInput.x
            );
        }
    }

    private void JumpFromWall()
    {
        isHanging = false;
        isJumpingFromWall = true;

        wallJumpTimer = wallJumpControlDelay;
        ledgeRegrabTimer = ledgeRegrabDelay;

        wallJumpHorizontalVelocity =
            -transform.forward * wallJumpBackForce;

        verticalVelocity = wallJumpUpForce;

        if (animator != null)
        {
            animator.SetBool(IsHangingHash, false);
            animator.SetFloat(HangDirectionHash, 0f);
            animator.SetTrigger(JumpFromWallHash);
        }

        if (showInputDebug)
        {
            Debug.Log(
                "[HOGARTH] Jumped from wall.",
                this
            );
        }
    }

    private void UpdateAnimator()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool(
            IsGroundedHash,
            characterController.isGrounded
        );

        animator.SetBool(
            IsHangingHash,
            isHanging
        );

        if (isHanging || isJumpingFromWall)
        {
            animator.SetFloat(SpeedHash, 0f);
            return;
        }

        float targetSpeed =
            moveInput.sqrMagnitude > 0.01f
                ? Mathf.Clamp01(moveInput.magnitude)
                : 0f;

        if (targetSpeed <= 0f)
        {
            animator.SetFloat(SpeedHash, 0f);
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
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawLedgeDebug)
        {
            return;
        }

        if (ledgeWallCheck == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(
            ledgeWallCheck.position,
            wallCheckDistance
        );
    }

    private void OnDisable()
    {
        ResetInputState();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            ResetInputState();
        }
    }

    private void ResetInputState()
    {
        moveInput = Vector2.zero;
        verticalVelocity = 0f;

        isHanging = false;
        isJumpingFromWall = false;

        wallJumpTimer = 0f;
        ledgeRegrabTimer = 0f;
        wallJumpHorizontalVelocity = Vector3.zero;

        if (animator != null)
        {
            animator.SetFloat(SpeedHash, 0f);
            animator.SetFloat(HangDirectionHash, 0f);

            animator.SetBool(
                IsGroundedHash,
                false
            );

            animator.SetBool(
                IsHangingHash,
                false
            );

            animator.ResetTrigger(
                JumpFromWallHash
            );
        }
    }
}