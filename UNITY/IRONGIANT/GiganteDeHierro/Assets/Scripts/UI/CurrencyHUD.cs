using TMPro;
using UnityEngine;

public class CurrencyHUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scrapText;

    private void Start()
    {
        if (CurrencyManager.Instance == null)
        {
            Debug.LogError("No existe CurrencyManager en la escena.");
            return;
        }

        CurrencyManager.Instance.OnScrapChanged += UpdateScrapText;
        UpdateScrapText(CurrencyManager.Instance.Scrap);
    }

    private void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnScrapChanged -= UpdateScrapText;
    }

    private void UpdateScrapText(int amount)
    {
        Debug.Log("HUD actualizado: " + amount);
        scrapText.text = amount.ToString();
    }
}