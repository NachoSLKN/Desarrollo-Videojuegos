
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransition
{
    public static string TargetScene { get; private set; }

    public static void Load(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("SceneTransition: Empty scene name.");
            return;
        }

        if (sceneName == "LoadingScreen")
        {
            Debug.LogError("SceneTransition: Cannot load LoadingScreen as destination.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError("SceneTransition: Scene not found in Build Profiles: " + sceneName);
            return;
        }

        TargetScene = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }
}
