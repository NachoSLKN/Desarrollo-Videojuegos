using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}