using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NachoSLKN
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        [SerializeField] int worldSceneIndex = 1; //Índice de la escena del mundo en el build settings

        private void Awake()
        {

            //Solo puede haber una instancia de este objeto por vez, si existen dos uno se destruye
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

        }


        private void Start()
        {
            DontDestroyOnLoad(gameObject); //No destruir este objeto al cambiar de escena
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return null;

        }
    }
}