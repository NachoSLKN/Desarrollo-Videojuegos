using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image imgFade;
    public float fadeDuration = 2f;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color c = imgFade.color;
        c.a = 1f;
        imgFade.color = c;

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            c.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
            imgFade.color = c;

            yield return null;
        }

        c.a = 0f;
        imgFade.color = c;
    }
}