using UnityEngine;

public class CrashPopup : MonoBehaviour
{
    [SerializeField] private float totalDuration = 0.5f;
    [SerializeField] private float fadeDuration = 0.2f;

    private SpriteRenderer spriteRenderer;
    private float timer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float fadeStart = totalDuration - fadeDuration;

        if (timer >= fadeStart && spriteRenderer != null)
        {
            float fadeProgress =
                (timer - fadeStart) / fadeDuration;

            Color color = originalColor;
            color.a = Mathf.Lerp(1f, 0f, fadeProgress);

            spriteRenderer.color = color;
        }

        if (timer >= totalDuration)
        {
            Destroy(gameObject);
        }
    }
}