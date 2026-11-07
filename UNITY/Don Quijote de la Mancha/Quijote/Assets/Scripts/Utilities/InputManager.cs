using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace NachoSLKN
{

    public class InputManager : MonoBehaviour
    {

        public static InputManager instance;
        private PlayerControlls playerControls;

        public PlayerManager player;

        [Header("Movement Position")]
        [SerializeField] Vector3 movementPosition;


        [Header("Inputs")]
        [SerializeField] private bool leftClick = false;

        private void Awake()
        {

            if (instance == null)
            {

                instance = this;
            }

            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

        }



        private void Start()
        {
            SceneManager.activeSceneChanged += OnSceneChange;

            // TEMPORALMENTE dejamos InputManager siempre activo para comprobar
            // si el problema viene de activarlo/desactivarlo entre escenas.
            // gameObject.SetActive(false);
        }


        private void OnSceneChange(Scene currentScene, Scene nextScene)
        {

            Debug.Log("CAMBIO DE ESCENA A: " + nextScene.name);


            // Esta función es llamada cada vez que cambiamos de escena.  
            Scene activeScene = SceneManager.GetActiveScene();


            // TEMPORALMENTE COMENTADO:
            // Si la escena es la escena de menú desactivamos el input manager.
            // Sino asumimos que cualquier otra escena es WorldScene.

            /*
            if (activeScene.buildIndex == WorldSaveGameManager.instance.menuSceneIndex)

            {

                gameObject.SetActive(false);

            }
            else
            {

                gameObject.SetActive(true);

            }
            */

        }

        private void OnEnable()
        {

            Debug.Log("INPUT MANAGER ENABLED");

            if (playerControls == null)

                playerControls = new PlayerControlls();

            // Inputs del mouse: Cuando hacemos left click pasamos a verdadero el boolean left click. 
            playerControls.Player.LeftClick.performed += i => leftClick = true;


            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();

        }


        private void Update()
        {
            HandleInputActions();
        }

        // Función maestra que contendrá todas las funciones en cada frame.
        private void HandleInputActions()
        {
            HandleLeftClickAction();
        }

        private void HandleLeftClickAction()
        {
            // Después de hacer click izq lo desactivamos inmediatamente.
            if (leftClick)
            {
                Debug.Log("LEFT CLICK DETECTADO");

                leftClick = false;

                if (player == null)
                {
                    Debug.LogError("PLAYER ES NULL");
                    return;
                }

                // Casteamos un raycast desde la cámara hacia la posición del ratón.
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    Debug.Log("RAYCAST HIT: " + hit.collider.name);

                    movementPosition = hit.point;

                    player.playerLocomotionManager.MovePlayerToPosition(movementPosition);
                }
                else
                {
                    Debug.LogError("RAYCAST NO HA GOLPEADO NADA");
                }
            }
        }

        private void OnDrawGizmos()
        {
            {

                // Si hemos clickado cualquier cosa y la posición sigue siendo 0,0,0 no hacer nada.
                if (movementPosition == Vector3.zero)
                    return;

                // Dibujar una esfera con un radio de 0.2f donde se haya clickado.
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(movementPosition, 0.2f);

            }
        }

    }
}