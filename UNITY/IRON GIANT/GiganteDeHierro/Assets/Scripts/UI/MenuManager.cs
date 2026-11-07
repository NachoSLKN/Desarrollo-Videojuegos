
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneTransition.Load("CharacterTest");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
