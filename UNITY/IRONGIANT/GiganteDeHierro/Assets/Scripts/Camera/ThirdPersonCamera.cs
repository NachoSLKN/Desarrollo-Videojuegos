using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform target;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 8f, -12f);

    [Header("Suavizado")]
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotationSpeed = 6f;

    [Header("Movimiento cámara")]
    [SerializeField] private float bobAmount = 0.05f;
    [SerializeField] private float bobSpeed = 3f;

    [Header("Paso gigante")]
    [SerializeField] private float stepShake = 0.08f;
    [SerializeField] private float stepShakeSpeed = 10f;

    private float bobTimer;
    private float shake;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition =
            target.position +
            target.rotation * offset;

        bool moving =
            Vector3.Distance(target.position, previousPosition) > 0.001f;

        if (moving)
        {
            bobTimer += Time.deltaTime * bobSpeed;

            desiredPosition.y += Mathf.Sin(bobTimer) * bobAmount;

            shake = Mathf.Lerp(
                shake,
                Mathf.Sin(Time.time * stepShakeSpeed) * stepShake,
                Time.deltaTime * 8f);

            desiredPosition += transform.right * shake;
        }
        else
        {
            bobTimer = 0f;
            shake = Mathf.Lerp(shake, 0f, Time.deltaTime * 6f);
        }

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime);

        Quaternion targetRotation =
            Quaternion.LookRotation(
                target.position + Vector3.up * 5f - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);

        previousPosition = target.position;
    }

    private Vector3 previousPosition;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (target != null)
        {
            previousPosition = target.position;
        }
    }
}