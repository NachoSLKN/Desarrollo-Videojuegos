using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenController : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string mainMenuScene = "MainMenu";

    [Header("Press Any Button Animation")]
    [SerializeField] private TMP_Text pressAnyButtonText;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseAmount = 0.06f;

    private Vector3 originalScale;
    private bool transitioning = false;

    private void Start()
    {
        if (pressAnyButtonText != null)
            originalScale = pressAnyButtonText.rectTransform.localScale;
    }

    private void Update()
    {
        if (transitioning)
            return;

        // Latido suave
        if (pressAnyButtonText != null)
        {
            float pulse = 1f + Mathf.Sin(Time.unscaledTime * pulseSpeed * Mathf.PI) * pulseAmount;

            pressAnyButtonText.rectTransform.localScale = originalScale * pulse;
        }

        bool keyboard = Keyboard.current != null &&
                        Keyboard.current.anyKey.wasPressedThisFrame;

        bool mouse = Mouse.current != null &&
                     (Mouse.current.leftButton.wasPressedThisFrame ||
                      Mouse.current.rightButton.wasPressedThisFrame);

        bool gamepad = false;

        if (Gamepad.current != null)
        {
            foreach (var control in Gamepad.current.allControls)
            {
                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    gamepad = true;
                    break;
                }
            }
        }

        if (keyboard || mouse || gamepad)
        {
            transitioning = true;
SceneTransition.Load(mainMenuScene);        
}
    }
}