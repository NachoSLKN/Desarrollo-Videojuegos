using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;



namespace NachoSLKN { 

public class WorldSaveGameManager : MonoBehaviour
{

        //Creación Singleton: Patrón de diseño que garantiza y controla una única instancia de una clase y proporciona un punto global de acceso a ella. 
        public static WorldSaveGameManager instance;


        [Header("World Index")]
        public int menuSceneIndex = 0;
        [SerializeField] int worldSceneIndex = 1;


        private void Awake()
        {

            //Si todavía no existe un WorldSaveGameManager, yo seré el Singleton.
            if(instance == null)
            {
                instance = this;
            }

            //Si ya existe otro, destruye este duplicado. 
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void StartNewGame()
        {
            Debug.Log("START NEW GAME EJECUTADO");

            var status = NetworkManager.Singleton.SceneManager.LoadScene(
                "World_Scene_01",
                LoadSceneMode.Single
            );

            Debug.Log("LOAD STATUS: " + status);
        }


    }


}

