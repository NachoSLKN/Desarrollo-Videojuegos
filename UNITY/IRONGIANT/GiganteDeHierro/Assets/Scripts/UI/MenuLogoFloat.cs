using UnityEngine;

public class MenuLogoFloat : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float verticalAmount = 6f;
    [SerializeField] private float movementSpeed = 1.2f;

    [Header("Scale")]
    [SerializeField] private float scaleAmount = 0.025f;
    [SerializeField] private float scaleSpeed = 1f;

    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private Vector3 initialScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        initialPosition = rectTransform.anchoredPosition;
        initialScale = rectTransform.localScale;
    }

    private void Update()
    {
        float verticalOffset =
            Mathf.Sin(Time.unscaledTime * movementSpeed)
            * verticalAmount;

        rectTransform.anchoredPosition =
            initialPosition +
            Vector2.up * verticalOffset;

        float scalePulse =
            1f +
            Mathf.Sin(Time.unscaledTime * scaleSpeed)
            * scaleAmount;

        rectTransform.localScale =
            initialScale * scalePulse;
    }
}