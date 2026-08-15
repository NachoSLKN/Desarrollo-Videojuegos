using UnityEngine;
using System.Collections;
using UnityEditor;

namespace NACHO
{
    [CustomEditor(typeof(AIWaypointNetwork))]
    public class AIWaypointNetworkEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AIWaypointNetwork network = (AIWaypointNetwork)target;

            network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup(
                "Display Mode",
                network.DisplayMode
            );

            if (network.DisplayMode == PathDisplayMode.Paths)
            {
                if (network.waypoints != null && network.waypoints.Count > 0)
                {
                    network.UIStart = EditorGUILayout.IntSlider(
                        "Start Waypoint",
                        network.UIStart,
                        0,
                        network.waypoints.Count - 1
                    );

                    network.UIEnd = EditorGUILayout.IntSlider(
                        "End Waypoint",
                        network.UIEnd,
                        0,
                        network.waypoints.Count - 1
                    );
                }
            }

            DrawDefaultInspector();
        }


        void OnSceneGUI()
        {
            AIWaypointNetwork network = (AIWaypointNetwork)target;

            if (network.waypoints == null || network.waypoints.Count == 0)
                return;


            // Renderiza todas las etiquetas
            for (int i = 0; i < network.waypoints.Count; i++)
            {
                if (network.waypoints[i] != null)
                {
                    Handles.Label(
                        network.waypoints[i].position,
                        "Waypoint " + i
                    );
                }
            }


            // Mostrar conexiones directas entre waypoints
            if (network.DisplayMode == PathDisplayMode.Connections)
            {
                if (network.waypoints.Count < 2)
                    return;

                Handles.color = Color.magenta;

                for (int i = 0; i < network.waypoints.Count - 1; i++)
                {
                    if (network.waypoints[i] != null &&
                        network.waypoints[i + 1] != null)
                    {
                        Handles.DrawLine(
                            network.waypoints[i].position,
                            network.waypoints[i + 1].position
                        );
                    }
                }


                // Cerrar el circuito: último -> primero
                if (network.waypoints[network.waypoints.Count - 1] != null &&
                    network.waypoints[0] != null)
                {
                    Handles.DrawLine(
                        network.waypoints[network.waypoints.Count - 1].position,
                        network.waypoints[0].position
                    );
                }
            }


            // Mostrar el camino calculado por NavMesh
            else if (network.DisplayMode == PathDisplayMode.Paths)
            {
                if (network.waypoints.Count < 2)
                    return;

                if (network.UIStart < 0 ||
                    network.UIStart >= network.waypoints.Count ||
                    network.UIEnd < 0 ||
                    network.UIEnd >= network.waypoints.Count)
                    return;

                if (network.waypoints[network.UIStart] == null ||
                    network.waypoints[network.UIEnd] == null)
                    return;


                UnityEngine.AI.NavMeshPath path =
                    new UnityEngine.AI.NavMeshPath();

                Vector3 from =
                    network.waypoints[network.UIStart].position;

                Vector3 to =
                    network.waypoints[network.UIEnd].position;


                UnityEngine.AI.NavMesh.CalculatePath(
                    from,
                    to,
                    UnityEngine.AI.NavMesh.AllAreas,
                    path
                );

                Handles.color = Color.yellow;

                if (path.corners != null && path.corners.Length > 1)
                {
                    Handles.DrawPolyLine(path.corners);
                }
            }
        }
    }
}