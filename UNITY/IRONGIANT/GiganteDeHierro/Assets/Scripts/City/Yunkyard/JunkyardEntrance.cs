using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class JunkyardEntrance : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string junkyardSceneName = "DeanJunkyard";

    [Header("Player Detection")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool loadOnTouch = true;

    [Header("Debug")]
    [SerializeField] private bool showDebug = true;

    private bool isLoading;

    private void Reset()
    {
        Collider triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;
    }

    private void Awake()
    {
        Collider triggerCollider = GetComponent<Collider>();

        if (!triggerCollider.isTrigger)
        {
            Debug.LogWarning(
                $"{name}: the Collider was not configured as a trigger. It has been corrected automatically.",
                this
            );

            triggerCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!loadOnTouch || isLoading)
        {
            return;
        }

        if (!other.CompareTag(playerTag))
        {
            return;
        }

        LoadJunkyard();
    }

    public void LoadJunkyard()
    {
        if (isLoading)
        {
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(junkyardSceneName))
        {
            Debug.LogError(
                $"The scene '{junkyardSceneName}' cannot be loaded. Add it to Build Profiles and verify the name.",
                this
            );

            return;
        }

        isLoading = true;

        if (showDebug)
        {
            Debug.Log($"Loading scene: {junkyardSceneName}", this);
        }

        SceneManager.LoadScene(junkyardSceneName);
    }
}