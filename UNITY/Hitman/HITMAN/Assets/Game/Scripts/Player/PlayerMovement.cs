using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Script References")]
    private InputManager inputManager;
    private Rigidbody playerRigidbody;

    [Header("Movement")]
    public Transform camObject;

    public float walkingSpeed = 2f;
    public float runningSpeed = 5f;
    public float rotationSpeed = 12f;

    [Header("Movement Flags")]
    public bool isRunning;
    public bool isMoving;

    private Vector3 moveDirection;

    private float characterHealth = 100f;
    public float presentHealth;

    [Header("Death")]
    public Animator animator;
    private bool isDead = false;

    [Header("Foot Steps")]
    public AudioSource leftFootAudioSource;
    public AudioSource rightFootAudioSource;
    public AudioClip[] footstepsSounds;
    public float walkingFootstepsInterval = 0.5f;
    public float runningFootstepsInterval = 0.35f;
    private float nextFootstepTime;
    private bool isLeftFootstep = true;

    [Header("Ground Check")]
    private bool isGrounded;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        presentHealth = characterHealth;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (camObject == null)
        {
            Debug.LogError(
                "PlayerMovement: Cam Object no está asignado.",
                this
            );

            return;
        }

        moveDirection =
            camObject.forward * inputManager.verticalInput +
            camObject.right * inputManager.horizontalInput;

        moveDirection.y = 0f;

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }

        isMoving = inputManager.moveAmount > 0.01f;

        float currentSpeed = 0f;

        if (isMoving)
        {
            currentSpeed = isRunning
                ? runningSpeed
                : walkingSpeed;
        }

        Vector3 horizontalVelocity =
            moveDirection * currentSpeed;

        // Conservamos la velocidad vertical del Rigidbody.
        Vector3 finalVelocity = new Vector3(
            horizontalVelocity.x,
            playerRigidbody.linearVelocity.y,
            horizontalVelocity.z
        );

        playerRigidbody.linearVelocity = finalVelocity;
    }

    private void HandleRotation()
    {
        // Al caminar hacia atrás mantenemos la orientación actual.
        if (
            inputManager.verticalInput < -0.01f &&
            Mathf.Abs(inputManager.horizontalInput) < 0.01f
        )
        {
            return;
        }

        Vector3 targetDirection =
            camObject.forward * inputManager.verticalInput +
            camObject.right * inputManager.horizontalInput;

        targetDirection.y = 0f;

        // No rotamos si no existe una dirección de movimiento.
        if (targetDirection.sqrMagnitude < 0.001f)
        {
            return;
        }

        targetDirection.Normalize();

        Quaternion targetRotation =
            Quaternion.LookRotation(targetDirection);

        Quaternion smoothRotation =
            Quaternion.Slerp(
                playerRigidbody.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

        playerRigidbody.MoveRotation(smoothRotation);
    }


    void Update()
    {
        isGrounded = Physics.Raycast(
            transform.position + Vector3.up * 0.1f,
            Vector3.down,
            0.3f
        );

        float footstepInterval =
            isRunning ? runningFootstepsInterval : walkingFootstepsInterval;

        // Si dejamos de movernos, cortamos inmediatamente los pasos
        if (!isMoving || !isGrounded)
        {
            leftFootAudioSource.Stop();
            rightFootAudioSource.Stop();

            nextFootstepTime = Time.time;
            return;
        }

        if (Time.time >= nextFootstepTime)
        {
            PlayFootstepsSound();
            nextFootstepTime = Time.time + footstepInterval;
        }
    }


    public void characterHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;

        if (presentHealth <= 0)
        {
            //animator.SetBool("Die", true);
            characterDie();
        }

    }


    //void characterDie()
    //{
    //    if (isDead)
    //        return;

    //    isDead = true;

    //    Debug.Log("Player Died");

    //    // Detenemos al jugador
    //    playerRigidbody.linearVelocity = Vector3.zero;

    //    // Animación de muerte
    //    if (animator != null)
    //    {
    //        animator.SetTrigger("Die");
    //    }

    //    StartCoroutine(ReturnToMainMenuAfterDeath());
    //}


    void characterDie()
    {
        Debug.Log("Player Died");

        SceneManager.LoadScene("MainMenu");
    }


    private void PlayFootstepsSound()
    {



        AudioSource footAudioSource = isLeftFootstep ? leftFootAudioSource : rightFootAudioSource;


        if (footstepsSounds.Length > 0)
        {
            AudioClip clip = footstepsSounds[Random.Range(0, footstepsSounds.Length)];
            footAudioSource.PlayOneShot(clip);
        }
        isLeftFootstep = !isLeftFootstep;   

    }

    private IEnumerator ReturnToMainMenuAfterDeath()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");
    }

}