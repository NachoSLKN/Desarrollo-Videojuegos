using UnityEngine;

public class Merchant : MonoBehaviour
{
    public GameObject interactionText;
    public GameObject merchantPanel;

    bool playerInside;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            merchantPanel.SetActive(!merchantPanel.activeSelf);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        interactionText.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
        interactionText.SetActive(false);
        merchantPanel.SetActive(false);
    }
}