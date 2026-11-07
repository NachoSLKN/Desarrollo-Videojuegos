using UnityEngine;

public class IronGiantWarArmToggle : MonoBehaviour
{
    [Header("Mallas del brazo normal")]
    [SerializeField] private GameObject[] normalArmParts;

    [Header("Piezas del brazo War")]
    [SerializeField] private GameObject[] warArmParts;

    [Header("Control")]
    [SerializeField] private KeyCode toggleKey = KeyCode.Q;

    private bool warMode;

    private void Start()
    {
        SetWarMode(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            SetWarMode(!warMode);
        }
    }

    private void SetWarMode(bool enabled)
    {
        warMode = enabled;

        foreach (GameObject part in normalArmParts)
        {
            if (part != null)
                part.SetActive(!enabled);
        }

        foreach (GameObject part in warArmParts)
        {
            if (part != null)
                part.SetActive(enabled);
        }
    }
}