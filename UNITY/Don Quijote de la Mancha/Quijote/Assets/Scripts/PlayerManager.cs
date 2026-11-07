
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace NachoSLKN
{
    public class PlayerManager : NetworkBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Animator animator;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            animator = GetComponent<Animator>();
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
        }

        private void Update()
        {
            if (IsOwner)
                playerLocomotionManager.HandleMovement();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            // WHEN THIS OBJECT SPAWNS, IF IT IS OUR PLAYER
            // (AND NOT ANOTHER PLAYER SPAWNING IN YOUR WORLD)
            // ASSIGN THIS PLAYER OBJECT TO THE INPUT MANAGER
            if (IsOwner)
            {
                InputManager.instance.player = this;
            }
            else
            {
                //transform.position = playerNetworkManager.networkPosition.Value;

                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    playerNetworkManager.networkPosition.Value,
                    ref playerNetworkManager.networkPositionVelocity,
                    playerNetworkManager.networkSmoothTime
                );

                //transform.position = playerNetworkManager.networkPosition.Value;

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    playerNetworkManager.networkRotation.Value,
                    Time.deltaTime / playerNetworkManager.networkSmoothTime
                );
            }

            playerNetworkManager.isMoving.OnValueChanged +=
                playerNetworkManager.OnIsMovingChanged;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            playerNetworkManager.isMoving.OnValueChanged -=
                playerNetworkManager.OnIsMovingChanged;
        }
    }
}
