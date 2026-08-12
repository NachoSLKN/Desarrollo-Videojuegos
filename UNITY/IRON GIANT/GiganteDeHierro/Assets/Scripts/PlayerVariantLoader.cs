using UnityEngine;

public class PlayerVariantLoader : MonoBehaviour
{
    [SerializeField] private GameObject normalPlayer;
    [SerializeField] private GameObject warPlayer;

    private void Start()
    {
        bool war =
            UpgradeManager.Instance != null &&
            UpgradeManager.Instance.CurrentVariant ==
            UpgradeManager.GiantVariant.LeftArmWar;

        normalPlayer.SetActive(!war);
        warPlayer.SetActive(war);
    }
}