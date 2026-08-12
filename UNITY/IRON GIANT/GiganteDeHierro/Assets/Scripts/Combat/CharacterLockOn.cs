using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLockOn : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Camera playerCamera;

    [Header("Búsqueda")]
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private float searchRadius = 40f;
    [SerializeField, Range(0f, 1f)]
    private float minimumScreenAlignment = 0.35f;

    [Header("Comportamiento")]
    [SerializeField] private bool rotateCharacterTowardsTarget;
    [SerializeField] private float characterRotationSpeed = 8f;

    private LockOnTarget currentTarget;

    public bool IsLockedOn => currentTarget != null;
    public Transform CurrentAimPoint =>
        currentTarget != null ? currentTarget.AimPoint : null;

    private void Awake()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    private void Update()
    {
        if (currentTarget == null)
            return;

        if (!currentTarget.gameObject.activeInHierarchy ||
            Vector3.Distance(transform.position,
                currentTarget.transform.position) > searchRadius)
        {
            ClearTarget();
            return;
        }

        if (rotateCharacterTowardsTarget)
            RotateTowardsCurrentTarget();
    }

    public void OnLockOn(InputValue value)
    {

        Debug.Log("Q pulsada");
        if (!value.isPressed)
            return;

        if (currentTarget != null)
        {
            ClearTarget();
            return;
        }

        currentTarget = FindBestTarget();

        if (currentTarget != null)
        {
            Debug.Log(
                $"Objetivo fijado: {currentTarget.name}",
                currentTarget
            );
        }
    }

    private LockOnTarget FindBestTarget()
    {
        if (playerCamera == null)
            return null;

        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            searchRadius,
            targetLayers,
            QueryTriggerInteraction.Collide
        );

        LockOnTarget bestTarget = null;
        float bestScore = float.MinValue;

        Vector3 cameraPosition = playerCamera.transform.position;
        Vector3 cameraForward = playerCamera.transform.forward;

        foreach (Collider targetCollider in colliders)
        {
            LockOnTarget candidate =
                targetCollider.GetComponentInParent<LockOnTarget>();

            if (candidate == null)
                continue;

            Vector3 direction =
                candidate.AimPoint.position - cameraPosition;

            float distance = direction.magnitude;

            if (distance <= Mathf.Epsilon)
                continue;

            direction.Normalize();

            float screenAlignment =
                Vector3.Dot(cameraForward, direction);

            if (screenAlignment < minimumScreenAlignment)
                continue;

            float score =
                screenAlignment * 2f -
                distance / searchRadius;

            if (score > bestScore)
            {
                bestScore = score;
                bestTarget = candidate;
            }
        }

        return bestTarget;
    }

    private void RotateTowardsCurrentTarget()
    {
        Vector3 direction =
            currentTarget.AimPoint.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            characterRotationSpeed * Time.deltaTime
        );
    }

    public void ClearTarget()
    {
        currentTarget = null;
    }
}