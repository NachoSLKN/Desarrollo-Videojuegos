using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public enum GiantVariant
    {
        Normal,
        LeftArmWar
    }

    public GiantVariant CurrentVariant { get; private set; }
        = GiantVariant.Normal;

    public bool LeftArmPurchased { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PurchaseAndEquipLeftArm()
    {
        LeftArmPurchased = true;
        CurrentVariant = GiantVariant.LeftArmWar;
    }

    public void EquipNormal()
    {
        CurrentVariant = GiantVariant.Normal;
    }

    public void EquipLeftArm()
    {
        if (LeftArmPurchased)
            CurrentVariant = GiantVariant.LeftArmWar;
    }
}