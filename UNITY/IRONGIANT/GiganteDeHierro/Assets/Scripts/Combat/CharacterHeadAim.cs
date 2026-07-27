using UnityEngine;

public class CharacterHeadAim : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterLockOn characterLockOn;

    [Header("Pesos de mirada")]
    [SerializeField, Range(0f, 1f)]
    private float totalWeight = 1f;

    [SerializeField, Range(0f, 1f)]
    private float bodyWeight = 0.15f;

    [SerializeField, Range(0f, 1f)]
    private float headWeight = 1f;

    [SerializeField, Range(0f, 1f)]
    private float eyesWeight = 0f;

    [SerializeField, Range(0f, 1f)]
    private float clampWeight = 0.65f;

    [Header("Suavizado")]
    [SerializeField] private float lookSpeed = 6f;

    private Vector3 currentLookPosition;
    private float currentWeight;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (characterLockOn == null)
        {
            characterLockOn = GetComponent<CharacterLockOn>();
        }

        if (animator == null)
        {
            Debug.LogError(
                "CharacterHeadAim: no se encontró ningún Animator.",
                this
            );

            enabled = false;
            return;
        }

        if (!animator.isHuman)
        {
            Debug.LogError(
                "CharacterHeadAim: el Animator no usa un Avatar Humanoid.",
                this
            );

            enabled = false;
        }
    }

    private void Update()
    {
        bool hasTarget =
            characterLockOn != null &&
            characterLockOn.IsLockedOn &&
            characterLockOn.CurrentAimPoint != null;

        float desiredWeight = hasTarget ? totalWeight : 0f;

        currentWeight = Mathf.MoveTowards(
            currentWeight,
            desiredWeight,
            lookSpeed * Time.deltaTime
        );

        if (hasTarget)
        {
            if (currentLookPosition == Vector3.zero)
            {
                currentLookPosition =
                    characterLockOn.CurrentAimPoint.position;
            }
            else
            {
                currentLookPosition = Vector3.Lerp(
                    currentLookPosition,
                    characterLockOn.CurrentAimPoint.position,
                    lookSpeed * Time.deltaTime
                );
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator == null)
            return;

        animator.SetLookAtWeight(
            currentWeight,
            bodyWeight,
            headWeight,
            eyesWeight,
            clampWeight
        );

        if (currentWeight > 0.001f)
        {
            animator.SetLookAtPosition(currentLookPosition);
        }
    }
}