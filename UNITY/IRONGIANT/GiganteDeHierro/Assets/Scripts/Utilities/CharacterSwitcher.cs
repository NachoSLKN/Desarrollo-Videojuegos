using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private GameObject ironGiant;
    [SerializeField] private GameObject hogarth;

    [Header("HUD")]
    [SerializeField] private GameObject giantPortrait;
    [SerializeField] private GameObject hogarthPortrait;
    [SerializeField] private GameObject giantHealthBar;
    [SerializeField] private GameObject hogarthHealthBar;

    [Header("Giant Abilities")]
    [SerializeField] private IronGiantEyeBeam giantEyeBeam;

    [Header("Lock On UI")]
    [SerializeField] private LockOnIndicatorUI lockOnIndicatorUI;

    private bool controllingGiant = true;

    private PlayerInput giantPlayerInput;
    private PlayerInput hogarthPlayerInput;

    private CharacterLockOn giantLockOn;
    private CharacterLockOn hogarthLockOn;

    [Header("Camera")]
    [SerializeField] private ThirdPersonCamera thirdPersonCamera;
    [SerializeField] private Transform giantCameraTarget;
    [SerializeField] private Transform hogarthCameraTarget;

    private void Update()
    {
        if (
            Keyboard.current != null &&
            Keyboard.current.tabKey.wasPressedThisFrame
        )
        {
            SwitchCharacter();
        }
    }



    private void Awake()
    {
        giantPlayerInput = ironGiant.GetComponent<PlayerInput>();
        hogarthPlayerInput = hogarth.GetComponent<PlayerInput>();

        giantLockOn = ironGiant.GetComponent<CharacterLockOn>();
        hogarthLockOn = hogarth.GetComponent<CharacterLockOn>();
    }

    private void Start()
    {
        SetActiveCharacter(true);
    }

    public void SwitchCharacter()
    {
        controllingGiant = !controllingGiant;
        SetActiveCharacter(controllingGiant);
    }

    private void SetActiveCharacter(bool useGiant)
    {
        giantPlayerInput.enabled = useGiant;
        hogarthPlayerInput.enabled = !useGiant;

        giantLockOn.ClearTarget();
        hogarthLockOn.ClearTarget();

        giantPortrait.SetActive(useGiant);
        giantHealthBar.SetActive(useGiant);

        hogarthPortrait.SetActive(!useGiant);
        hogarthHealthBar.SetActive(!useGiant);

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.SetTarget(
                useGiant ? giantCameraTarget : hogarthCameraTarget
            );
        }

        if (lockOnIndicatorUI != null)
        {
            lockOnIndicatorUI.SetCharacterLockOn(
                useGiant ? giantLockOn : hogarthLockOn
            );
        }
    }
}