using UnityEngine;
using UnityEngine.UI;

public class MenuEyePulse : MonoBehaviour
{
    [Header("Eyes")]
    [SerializeField] private Graphic leftEye;
    [SerializeField] private Graphic rightEye;

    [Header("Pulse")]
    [SerializeField] private float minIntensity = 0.65f;
    [SerializeField] private float maxIntensity = 1.25f;
    [SerializeField] private float pulseSpeed = 2f;

    [Header("Color")]
    [SerializeField]
    private Color eyeColor =
        new Color(1f, 0.95f, 0.7f, 1f);

    private void Update()
    {
        float pulse =
            Mathf.Lerp(
                minIntensity,
                maxIntensity,
                (Mathf.Sin(Time.unscaledTime * pulseSpeed) + 1f) * 0.5f
            );

        Color currentColor = eyeColor * pulse;
        currentColor.a = eyeColor.a;

        if (leftEye != null)
        {
            leftEye.color = currentColor;
        }

        if (rightEye != null)
        {
            rightEye.color = currentColor;
        }
    }
}