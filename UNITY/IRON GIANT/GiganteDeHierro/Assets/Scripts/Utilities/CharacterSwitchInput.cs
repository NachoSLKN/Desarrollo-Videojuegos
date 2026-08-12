using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitchInput : MonoBehaviour
{
    [SerializeField] private CharacterSwitcher characterSwitcher;


    public void OnSwitchCharacter(InputValue value)
    {
        if (!value.isPressed || characterSwitcher == null)
            return;

        characterSwitcher.SwitchCharacter();
    }
}