using UnityEngine;

public class ScrapPickup : MonoBehaviour
{
    [SerializeField] private int scrapValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddScrap(scrapValue);
        }

        Destroy(gameObject);
    }
}