using UnityEngine;

public class LockOnIndicatorUI : MonoBehaviour
{
    [Header("Sistema de fijado")]
    [SerializeField] private CharacterLockOn characterLockOn;

    [Header("UI")]
    [SerializeField] private RectTransform indicator;

    [Header("Cámara")]
    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (indicator != null)
        {
            indicator.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (characterLockOn == null ||
            indicator == null ||
            playerCamera == null)
        {
            return;
        }

        Transform aimPoint = characterLockOn.CurrentAimPoint;

        if (!characterLockOn.IsLockedOn || aimPoint == null)
        {
            indicator.gameObject.SetActive(false);
            return;
        }

        Vector3 screenPosition =
            playerCamera.WorldToScreenPoint(aimPoint.position);

        bool targetIsVisible = screenPosition.z > 0f;

        indicator.gameObject.SetActive(targetIsVisible);

        if (!targetIsVisible)
        {
            return;
        }

        indicator.position = screenPosition;
    }

    public void SetCharacterLockOn(CharacterLockOn newCharacterLockOn)
    {
        characterLockOn = newCharacterLockOn;

        if (indicator != null)
        {
            indicator.gameObject.SetActive(false);
        }
    }
}