using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DestructibleBlock : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 1f;

    [Header("Destruction")]
    [SerializeField] private float explosionForce = 8f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float upwardModifier = 0.8f;

    [Header("Cleanup")]
    [SerializeField] private float destroyDelay = 6f;

    private float currentHealth;
    private Rigidbody blockRigidbody;
    private bool destroyed;

    private void Awake()
    {
        currentHealth = maxHealth;

        blockRigidbody = GetComponent<Rigidbody>();
        blockRigidbody.isKinematic = true;
        blockRigidbody.useGravity = false;
    }

    public void TakeBeamDamage(
        float damage,
        Vector3 hitPoint,
        Vector3 beamDirection
    )
    {
        if (destroyed)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0f)
            BreakBlock(hitPoint, beamDirection);
    }

    private void BreakBlock(
        Vector3 hitPoint,
        Vector3 beamDirection
    )
    {
        destroyed = true;

        if (CurrencyManager.Instance == null)
        {
            Debug.LogError("NO EXISTE CurrencyManager.Instance");
        }
        else
        {
            CurrencyManager.Instance.AddScrap(10);
            Debug.Log("Tornillos totales: " + CurrencyManager.Instance.Scrap);
        }

        blockRigidbody.isKinematic = false;
        blockRigidbody.useGravity = true;

        Vector3 forceOrigin =
            hitPoint - beamDirection.normalized;

        blockRigidbody.AddExplosionForce(
            explosionForce,
            forceOrigin,
            explosionRadius,
            upwardModifier,
            ForceMode.Impulse
        );

        Destroy(gameObject, destroyDelay);
    }
}