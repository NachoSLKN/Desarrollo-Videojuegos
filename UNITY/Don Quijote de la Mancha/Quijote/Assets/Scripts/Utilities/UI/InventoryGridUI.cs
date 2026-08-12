using UnityEngine;

public class InventoryGridUI : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField, Min(1)] private int rows = 6;
    [SerializeField, Min(1)] private int columns = 8;

    private void Awake()
    {
        GenerateSlots();
    }

    private void GenerateSlots()
    {
        if (slotPrefab == null)
        {
            Debug.LogError("InventoryGridUI: falta asignar Slot Prefab.", this);
            return;
        }

        ClearExistingSlots();

        int totalSlots = rows * columns;

        for (int index = 0; index < totalSlots; index++)
        {
            GameObject slot = Instantiate(slotPrefab, transform);
            slot.name = $"InventorySlot_{index:00}";
        }
    }

    private void ClearExistingSlots()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}