using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeanShopUI : MonoBehaviour
{
    [Header("Precio")]
    [SerializeField] private int leftArmPrice = 50;

    [Header("UI")]
    [SerializeField] private TMP_Text selectedUpgradeText;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Button buyButton;

    [Header("Escena de juego")]
    [SerializeField] private string gameplaySceneName = "CharacterTest";

    private bool leftArmSelected;

    private void Start()
    {
        selectedUpgradeText.text = "NINGUNA MEJORA SELECCIONADA";
        statusText.text = string.Empty;
        buyButton.interactable = false;
    }

    public void SelectLeftArmUpgrade()
    {
        leftArmSelected = true;

        selectedUpgradeText.text =
            $"BRAZO IZQUIERDO WAR - {leftArmPrice} TORNILLOS";

        statusText.text = string.Empty;
        buyButton.interactable = true;
    }

    public void BuySelectedUpgrade()
    {
        if (!leftArmSelected)
        {
            statusText.text = "SELECCIONA UNA MEJORA";
            return;
        }

        if (CurrencyManager.Instance == null)
        {
            statusText.text = "NO EXISTE CURRENCY MANAGER";
            return;
        }

        if (UpgradeManager.Instance == null)
        {
            statusText.text = "NO EXISTE UPGRADE MANAGER";
            return;
        }

        if (UpgradeManager.Instance.LeftArmPurchased)
        {
            UpgradeManager.Instance.EquipLeftArm();
            statusText.text = "MEJORA EQUIPADA";

            SceneManager.LoadScene(gameplaySceneName);
            return;
        }

        if (!CurrencyManager.Instance.TrySpendScrap(leftArmPrice))
        {
            statusText.text = "NO TIENES SUFICIENTES TORNILLOS";
            return;
        }

        UpgradeManager.Instance.PurchaseAndEquipLeftArm();

        statusText.text = "MEJORA COMPRADA Y EQUIPADA";
        buyButton.interactable = false;

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void ReturnToGameplay()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}