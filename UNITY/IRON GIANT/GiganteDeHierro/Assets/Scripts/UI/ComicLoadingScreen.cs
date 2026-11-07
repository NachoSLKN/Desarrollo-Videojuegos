
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComicLoadingScreen : MonoBehaviour
{
    [Header("Comic Covers")]
    [SerializeField] private Image comicImage;
    [SerializeField] private Sprite[] comicCovers;

    [Header("Loading UI")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text loadingText;

    [Header("Settings")]
    [SerializeField] private float minimumDisplayTime = 2f;

    private void Start()
    {
        // Seleccionar una portada aleatoria
        if (comicImage != null && comicCovers != null && comicCovers.Length > 0)
        {
            int index = Random.Range(0, comicCovers.Length);
            comicImage.sprite = comicCovers[index];
        }

        if (progressBar != null)
            progressBar.value = 0f;

        StartCoroutine(LoadTargetScene());
    }

    private IEnumerator LoadTargetScene()
    {
        string destination = SceneTransition.TargetScene;

        if (string.IsNullOrEmpty(destination))
        {
            Debug.LogError("LoadingScreen: No destination scene specified.");
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(destination);

        if (operation == null)
        {
            Debug.LogError("LoadingScreen: Could not load " + destination);
            yield break;
        }

        operation.allowSceneActivation = false;

        float elapsed = 0f;

        while (operation.progress < 0.9f || elapsed < minimumDisplayTime)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (progressBar != null)
                progressBar.value = progress;

            if (loadingText != null)
                loadingText.text = $"LOADING... {Mathf.RoundToInt(progress * 100)}%";

            yield return null;
        }

        if (progressBar != null)
            progressBar.value = 1f;

        if (loadingText != null)
            loadingText.text = "LOADING... 100%";

        operation.allowSceneActivation = true;
    }
}
