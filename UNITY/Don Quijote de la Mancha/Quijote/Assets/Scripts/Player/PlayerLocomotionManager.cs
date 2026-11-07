using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace NachoSLKN
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        PlayerManager player;
        private Coroutine movementCoroutine;
        // [SerializeField] private float maxNavmeshAgentMagnitudeDifference = 0.2f;

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        public void HandleMovement()
        {
            SyncCharacterWithAgent();
        }

        public void MovePlayerToPosition(Vector3 newPosition)
        {
            NavMeshAgent agent = player.navMeshAgent;
            if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            {
                Debug.LogWarning("No se puede mover: NavMeshAgent no esta activo sobre un NavMesh.");
                return;
            }

            if (!agent.SetDestination(newPosition))
            {
                Debug.LogWarning("NavMeshAgent no ha aceptado el destino.");
                return;
            }

            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);

            player.playerNetworkManager.isMoving.Value = true;
            movementCoroutine = StartCoroutine(HasReachedDestinationCoroutine());
        }

        private IEnumerator HasReachedDestinationCoroutine()
        {
            NavMeshAgent agent = player.navMeshAgent;
            while (agent != null && agent.enabled && agent.isOnNavMesh)
            {
                if (!agent.pathPending)
                {
                    if (agent.pathStatus == NavMeshPathStatus.PathInvalid ||
                        (!agent.hasPath && agent.velocity.sqrMagnitude < 0.01f) ||
                        (agent.remainingDistance <= agent.stoppingDistance &&
                         agent.velocity.sqrMagnitude < 0.01f))
                        break;
                }
                yield return null;
            }

            player.playerNetworkManager.isMoving.Value = false;
            movementCoroutine = null;
        }

        private void SyncCharacterWithAgent()
        {
            NavMeshAgent agent = player.navMeshAgent;
            if (agent == null || !agent.enabled || !agent.isOnNavMesh)
                return;

            // Guardamos la posicion y rotacion del agente ANTES de mover su padre.
            Vector3 agentWorldPosition = agent.transform.position;
            Quaternion agentWorldRotation = agent.transform.rotation;

            // El agente calcula la ruta; el personaje sigue su posicion.
            player.transform.position = agentWorldPosition;

            // Giramos el personaje hacia el desplazamiento real, no copiando
            // la rotacion del hijo despues de haber girado el padre.
            Vector3 direction = agent.desiredVelocity;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
                player.transform.rotation = Quaternion.RotateTowards(
                    player.transform.rotation,
                    targetRotation,
                    agent.angularSpeed * Time.deltaTime
                );
            }

            // Al mover/rotar el padre, Unity tambien transforma al hijo.
            // Restauramos ambos valores globales para no alterar su navegacion.
            agent.transform.SetPositionAndRotation(agentWorldPosition, agentWorldRotation);

            // Sincronizacion provisional con el Animator del objeto raiz.
            bool isMoving = player.playerNetworkManager.isMoving.Value;
            player.animator.SetBool("isMoving", isMoving);
            player.animator.SetFloat("Speed", isMoving ? 1f : 0f);

            // Codigo anterior conservado como referencia:
            // Vector3 positionDifference = agent.transform.position - player.transform.position;
            // if (positionDifference.magnitude > maxNavmeshAgentMagnitudeDifference)
            //     agent.transform.localPosition = Vector3.zero;
            // player.transform.position = agent.transform.position;
            // if (player.playerNetworkManager.isMoving.Value)
            //     player.transform.rotation = agent.transform.rotation;
            // agent.transform.localPosition = Vector3.zero;
        }
    }
}
