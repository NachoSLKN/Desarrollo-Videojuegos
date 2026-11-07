using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace NachoSLKN
{

    public class TitleScreenManager : MonoBehaviour
    {
        [Header("Press Start Menu")]
        [SerializeField] CanvasGroup pressStartMenuCanvasGroup;
        private Coroutine fadeOutPressStartMenuCoroutine;
        [SerializeField] float fadeTime = 1.0f;


        [Header("Character Menu")]
        [SerializeField] CanvasGroup characterMenuCanvasGroup; 
        [SerializeField]  GameObject[] characterMenuButtons;
        private Coroutine fadeInCharacterMenuCoroutine;



        // PRESS START MENU
        public void StartGame()
        {
            Debug.Log("START GAME PULSADO");

            NetworkManager.Singleton.StartHost();
            FadeOutPressStartMenu();
            FadeInCharacterMenu();
        }

        private void FadeOutPressStartMenu()
        {

            if (fadeOutPressStartMenuCoroutine != null)
                StopCoroutine(fadeOutPressStartMenuCoroutine);

            fadeOutPressStartMenuCoroutine = StartCoroutine(FadeOutPressStartMenuCoroutine(fadeTime));

        }

        private IEnumerator FadeOutPressStartMenuCoroutine(float durationOfFade)
        {
            float timeElapsed = 0;
            float startingAlpha = pressStartMenuCanvasGroup.alpha;

            while(timeElapsed < durationOfFade)
            {
                pressStartMenuCanvasGroup.alpha = Mathf.Lerp(startingAlpha, 0, timeElapsed / durationOfFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }



        // CHARACTER MENU
        public void StartNewGame()
        {
            Debug.Log("NEW GAME PULSADO");
            WorldSaveGameManager.instance.StartNewGame();
        }
        private void FadeInCharacterMenu()
        {

            if (fadeInCharacterMenuCoroutine != null)
                StopCoroutine(fadeInCharacterMenuCoroutine);

            fadeInCharacterMenuCoroutine = StartCoroutine(FadeInCharacterMenuCoroutine(fadeTime));

            for (int i = 0;     i < characterMenuButtons.Length; i++)
            {
                if (characterMenuButtons[i] == null)
                    continue;
                characterMenuButtons[i].SetActive(true);
            }

        }

        private IEnumerator FadeInCharacterMenuCoroutine(float durationOfFade)
        {
            float timeElapsed = 0;
            float startingAlpha = 0;

            while (timeElapsed < durationOfFade)
            {
                Mathf.Lerp(startingAlpha, 1, timeElapsed / durationOfFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }


            characterMenuCanvasGroup.alpha = 1f;


        }


       

       

    }
}