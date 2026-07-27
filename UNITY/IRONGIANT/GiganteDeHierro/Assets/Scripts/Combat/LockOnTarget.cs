using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    [Header("Punto al que apuntará el fijado")]
    [SerializeField] private Transform aimPoint;

    [Header("¿Es un enemigo?")]
    [SerializeField] private bool hostile = true;

    public Transform AimPoint => aimPoint != null ? aimPoint : transform;
    public bool IsHostile => hostile;

    private void Reset()
    {
        aimPoint = transform;
    }
}