using UnityEngine;
using UnityEngine.InputSystem;

public class IronGiantCannon : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Input")]
    [SerializeField] private Key fireKey = Key.C;

    private static readonly int FireCannonHash =
        Animator.StringToHash("FireCannon");

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError(
                "IronGiantCannon: No se encontró ningún Animator.",
                this
            );
        }
    }

    private void Update()
    {
        if (animator == null)
            return;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current[fireKey].wasPressedThisFrame)
        {
            animator.SetTrigger(FireCannonHash);
        }
    }
}