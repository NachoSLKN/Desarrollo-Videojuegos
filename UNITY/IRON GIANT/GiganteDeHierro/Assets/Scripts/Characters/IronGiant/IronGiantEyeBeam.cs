using UnityEngine;
using UnityEngine.InputSystem;

public class IronGiantEyeBeam : MonoBehaviour
{
    [Header("Beam Origins")]
    [SerializeField] private Transform leftEyeOrigin;
    [SerializeField] private Transform rightEyeOrigin;

    [SerializeField] private Transform beamDirectionReference;

    [Header("Beam Renderers")]
    [SerializeField] private LineRenderer leftEyeCore;
    [SerializeField] private LineRenderer leftEyeGlow;

    [SerializeField] private LineRenderer rightEyeCore;
    [SerializeField] private LineRenderer rightEyeGlow;

    [Header("Beam Settings")]
    [SerializeField] private float beamDistance = 100f;
    [SerializeField] private LayerMask hitLayers = ~0;

    [Header("Electric Effect")]
    [SerializeField] private int beamPoints = 18;
    [SerializeField] private float coreJitter = 0.025f;
    [SerializeField] private float glowJitter = 0.08f;

    [Header("Debug")]
    [SerializeField] private bool showDebug = true;

    [Header("Damage")]
    [SerializeField] private float damagePerSecond = 3f;

    [Header("Lock On")]
    [SerializeField] private CharacterLockOn characterLockOn;


    private bool isFiring;

    private void Awake()
    {
        SetBeamEnabled(false);
    }

    private void Update()
    {
        bool shouldFire =
            Mouse.current != null &&
            Mouse.current.rightButton.isPressed;

        if (shouldFire != isFiring)
        {
            isFiring = shouldFire;
            SetBeamEnabled(isFiring);

            if (showDebug)
            {
                Debug.Log($"[EYE BEAM] Firing: {isFiring}", this);
            }
        }

        if (isFiring)
        {
            UpdateEyeBeam(
                leftEyeOrigin,
                leftEyeCore,
                leftEyeGlow
            );

            UpdateEyeBeam(
                rightEyeOrigin,
                rightEyeCore,
                rightEyeGlow
            );
        }
    }

    private void UpdateEyeBeam(
     Transform eyeOrigin,
     LineRenderer core,
     LineRenderer glow
 )
    {
        if (
            eyeOrigin == null ||
            core == null ||
            glow == null
        )
        {
            return;
        }

        Vector3 startPosition = eyeOrigin.position;
        Vector3 direction;

        if (
            characterLockOn != null &&
            characterLockOn.IsLockedOn &&
            characterLockOn.CurrentAimPoint != null
        )
        {
            direction =
                characterLockOn.CurrentAimPoint.position -
                startPosition;

            direction.Normalize();
        }
        else
        {
            direction =
                beamDirectionReference != null
                    ? beamDirectionReference.forward
                    : transform.forward;
        }

        Vector3 endPosition =
            startPosition + direction * beamDistance;

        if (
            Physics.Raycast(
                startPosition,
                direction,
                out RaycastHit hit,
                beamDistance,
                hitLayers,
                QueryTriggerInteraction.Ignore
            )
        )
        {
            endPosition = hit.point;

            DestructibleBlock destructibleBlock =
                hit.collider.GetComponentInParent<DestructibleBlock>();

            if (destructibleBlock != null)
            {
                destructibleBlock.TakeBeamDamage(
                    damagePerSecond * Time.deltaTime,
                    hit.point,
                    direction
                );
            }
        }

        UpdateElectricLine(
            core,
            startPosition,
            endPosition,
            coreJitter
        );

        UpdateElectricLine(
            glow,
            startPosition,
            endPosition,
            glowJitter
        );
    }

    private void UpdateElectricLine(
        LineRenderer lineRenderer,
        Vector3 startPosition,
        Vector3 endPosition,
        float jitter
    )
    {
        int safePointCount = Mathf.Max(2, beamPoints);

        lineRenderer.positionCount = safePointCount;

        Vector3 beamDirection =
            (endPosition - startPosition).normalized;

        Vector3 perpendicularA =
            Vector3.Cross(
                beamDirection,
                Vector3.up
            );

        if (perpendicularA.sqrMagnitude < 0.001f)
        {
            perpendicularA =
                Vector3.Cross(
                    beamDirection,
                    Vector3.right
                );
        }

        perpendicularA.Normalize();

        Vector3 perpendicularB =
            Vector3.Cross(
                beamDirection,
                perpendicularA
            ).normalized;

        for (int i = 0; i < safePointCount; i++)
        {
            float t =
                i / (float)(safePointCount - 1);

            Vector3 point =
                Vector3.Lerp(
                    startPosition,
                    endPosition,
                    t
                );

            if (i != 0 && i != safePointCount - 1)
            {
                float randomA =
                    Random.Range(-jitter, jitter);

                float randomB =
                    Random.Range(-jitter, jitter);

                point +=
                    perpendicularA * randomA +
                    perpendicularB * randomB;
            }

            lineRenderer.SetPosition(i, point);
        }
    }

    private void SetBeamEnabled(bool enabled)
    {
        if (leftEyeCore != null)
            leftEyeCore.enabled = enabled;

        if (leftEyeGlow != null)
            leftEyeGlow.enabled = enabled;

        if (rightEyeCore != null)
            rightEyeCore.enabled = enabled;

        if (rightEyeGlow != null)
            rightEyeGlow.enabled = enabled;
    }

    private void OnDisable()
    {
        isFiring = false;
        SetBeamEnabled(false);
    }

}