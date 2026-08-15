using UnityEngine;
using UnityEngine.AI;
using System.Collections; 


[RequireComponent(typeof(NavMeshAgent))]

        public class NavAgentExample : MonoBehaviour
{


    //Inspector Assigned Variable
    public AIWaypointNetwork WaypointNetwork = null;
    public int CurrentIndex = 0;
    public bool HastPath = false;
    public bool PathPending = false;
    public bool PathStale = false;
    public NavMeshPathStatus PathStatus = NavMeshPathStatus.PathInvalid;
    public AnimationCurve JumpCurve = new AnimationCurve(); //Unity type que permite trabajar con curvas en el inspector

    //Private Members
    private NavMeshAgent _navAgent = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.updatePosition = true;
        _navAgent.updateRotation = true;

        if (WaypointNetwork == null) return;
        SetNextDestination(false); 

        //if (WaypointNetwork.waypoints[CurrentIndex] != null)
        //{
        //    _navAgent.destination = WaypointNetwork.waypoints[CurrentIndex].position;
        //}


    }


    void SetNextDestination (bool increment)
    {


        //If NO network return
        if (!WaypointNetwork) return; 


        //Calcula cuánto necesita el index del waypoint para ser incrementado. 
        int incStep = increment?1:0;
        Transform nextWaypointTransform = null; 


        while (nextWaypointTransform == null)
        {

            //Calcula el index del siguiente Waypoint factorizando el incremento con wrap-around y fecth waypoint.
            int nextWaypoint = (CurrentIndex+incStep>= WaypointNetwork.waypoints.Count) ? 0 : CurrentIndex+incStep;
            nextWaypointTransform = WaypointNetwork.waypoints[nextWaypoint];


            //Asumiendo que tenemos un waypoint transform válido,
            if (nextWaypointTransform != null)
            {
                 //Actualizamos el index del waypoint actual, asignando su posición (NavMeshAgents).
                CurrentIndex = nextWaypoint;
                _navAgent.destination = nextWaypointTransform.position;
                return; 
            }

        }

        //No hemos encontrado un waypoint válido en la lista para esta iteración. 
        CurrentIndex++;

    }



    void Update()
    {


        //Copia los valores de estado de NavMeshAgents en variables visibles del inspector
        HastPath = _navAgent.hasPath;
        PathPending = _navAgent.pathPending;
        PathStale = _navAgent.isPathStale;
        PathStatus = _navAgent.pathStatus;



        if (_navAgent.isOnOffMeshLink)
        {

            StartCoroutine(Jump(2.0f));
                return;
        
        }


        //Si no tenemos un camino y no hay uno pendiente, entonces settea el siguiente waypoint como objetivo, de lo contrario si el camino es stale regenera el path. 
        if ((_navAgent.remainingDistance<=_navAgent.stoppingDistance && !PathPending) || PathStatus == NavMeshPathStatus.PathInvalid || PathStatus==NavMeshPathStatus.PathPartial)
            SetNextDestination(true);
        else 
        if(_navAgent.isPathStale)
           SetNextDestination(false);
    
          
    
    
    }


    IEnumerator Jump( float duration )
    {

        OffMeshLinkData data = _navAgent.currentOffMeshLinkData;
        Vector3 startPos = _navAgent.transform.position;
        Vector3 endPos = data.endPos + (_navAgent.baseOffset * Vector3.up);
        float time = 0.0f;

        while(time <= duration)
        {
            float t = time / duration;

            //Interpolamos entre la posición de inicio y posición final, offseteando la altura por cierto grado.
            _navAgent.transform.position = Vector3.Lerp(startPos, endPos, t) + (JumpCurve.Evaluate(t) * Vector3.up) ;
            time += Time.deltaTime; 
            yield return null;




        }

        _navAgent.CompleteOffMeshLink();


    }


}
