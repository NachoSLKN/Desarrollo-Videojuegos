// INSTRUCCIONES DE INSTALACION:
// 1) Copia este archivo en Assets/Editor/GestorXRModosEditor.cs.
// 2) Evita tener scripts duplicados con la clase GestorXRModosEditor.
// 3) Cambia de modo SIEMPRE fuera de Play.
//
// Unity 6.3 + XR Plug-in Management + XR Interaction Toolkit.
// Automatiza ajustes del PROYECTO Unity. No inicia la app Meta Horizon Link
// ni cambia el runtime OpenXR de Windows, que se configura fuera de Unity.

using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.XR.Management;
using UnityEditor.XR.Management.Metadata;
using UnityEngine;
using UnityEngine.XR.Management;

public static class GestorXRModosEditor
{
    private const BuildTargetGroup Plataforma = BuildTargetGroup.Standalone;
    private const string OpenXRLoader = "UnityEngine.XR.OpenXR.OpenXRLoader";
    private const string MockHMDLoader = "Unity.XR.MockHMD.MockHMDLoader";

    [MenuItem("Gestor XR/1 - Simulador (teclado y raton)")]
    private static void SeleccionarSimulador()
    {
        if (!PuedeCambiar()) return;
        if (!ObtenerXR(out XRGeneralSettings general, out XRManagerSettings manager)) return;

        // El simulador XRI proporciona entradas para visor y mandos por si solo.
        // OpenXR puede seguir asignado al proyecto, pero NO se inicia.
        XRPackageMetadataStore.RemoveLoader(manager, MockHMDLoader, Plataforma);
        if (!ConfigurarInicioXR(general, false)) return;
        ConfigurarSimuladores(true);
        GuardarCambios(general, manager);
        Debug.Log("Gestor XR: SIMULADOR. Initialize XR on Startup OFF; XR Device Simulator ON. Pulsa Play.");
    }

    [MenuItem("Gestor XR/2 - Meta Quest Link (gafas reales)")]
    private static void SeleccionarQuestLink()
    {
        if (!PuedeCambiar()) return;
        if (!ObtenerXR(out XRGeneralSettings general, out XRManagerSettings manager)) return;

        // Un unico proveedor para PC VR: OpenXR (no Mock HMD).
        XRPackageMetadataStore.RemoveLoader(manager, MockHMDLoader, Plataforma);
        bool asignado = manager.activeLoaders.Any(loader =>
            loader != null && loader.GetType().FullName == OpenXRLoader);
        if (!asignado && !XRPackageMetadataStore.AssignLoader(manager, OpenXRLoader, Plataforma))
        {
            Debug.LogError("No se pudo habilitar OpenXR. Comprueba que OpenXR Plugin este instalado.");
            return;
        }

        if (!ConfigurarInicioXR(general, true)) return;
        ConfigurarSimuladores(false);
        GuardarCambios(general, manager);
        Debug.Log("Gestor XR: QUEST LINK. Initialize XR on Startup ON; OpenXR ON; simulador OFF. " +
                  "Abre Meta Horizon Link, conecta/activa Quest Link dentro de las gafas y pulsa Play.");
    }

    [MenuItem("Gestor XR/3 - Mock HMD (experimental)")]
    private static void SeleccionarMockHMD()
    {
        if (!PuedeCambiar()) return;
        if (!ObtenerXR(out XRGeneralSettings general, out XRManagerSettings manager)) return;

        XRPackageMetadataStore.RemoveLoader(manager, OpenXRLoader, Plataforma);
        bool asignado = manager.activeLoaders.Any(loader =>
            loader != null && loader.GetType().FullName == MockHMDLoader);
        if (!asignado && !XRPackageMetadataStore.AssignLoader(manager, MockHMDLoader, Plataforma))
        {
            Debug.LogError("No se pudo habilitar Mock HMD. Comprueba que este paquete este instalado.");
            return;
        }

        if (!ConfigurarInicioXR(general, true)) return;
        ConfigurarSimuladores(true);
        GuardarCambios(general, manager);
        Debug.LogWarning("Gestor XR: MOCK HMD (experimental). No es necesario para usar el simulador de mandos.");
    }

    [MenuItem("Gestor XR/Ver estado actual")]
    private static void MostrarEstado()
    {
        if (!ObtenerXR(out XRGeneralSettings general, out XRManagerSettings manager)) return;
        string proveedores = string.Join(", ", manager.activeLoaders
            .Where(loader => loader != null).Select(loader => loader.GetType().Name));
        bool simuladorActivo = BuscarSimuladores().Any(go => go.activeInHierarchy);
        Debug.Log("XR al iniciar: " + general.InitManagerOnStart +
                  " | Loaders: " + proveedores + " | Simulador en escena activo: " + simuladorActivo);
    }

    private static bool PuedeCambiar()
    {
        if (!EditorApplication.isPlayingOrWillChangePlaymode) return true;
        Debug.LogWarning("Sal de Play antes de cambiar el modo Gestor XR.");
        return false;
    }

    private static bool ObtenerXR(out XRGeneralSettings general, out XRManagerSettings manager)
    {
        general = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(Plataforma);
        manager = general != null ? general.AssignedSettings : null;
        if (manager != null) return true;
        Debug.LogError("No se ha encontrado XR Plug-in Management para Windows/Standalone.");
        return false;
    }

    // InitManagerOnStart es de solo lectura en XR Management; la casilla
    // del Inspector se guarda en el campo serializado m_InitManagerOnStart.
    private static bool ConfigurarInicioXR(XRGeneralSettings general, bool iniciar)
    {
        SerializedObject objeto = new SerializedObject(general);
        SerializedProperty propiedad = objeto.FindProperty("m_InitManagerOnStart");
        if (propiedad == null || propiedad.propertyType != SerializedPropertyType.Boolean)
        {
            Debug.LogError("Tu version de XR Management no expone m_InitManagerOnStart. " +
                           "Configura Initialize XR on Startup manualmente en Project Settings.");
            return false;
        }
        objeto.Update();
        propiedad.boolValue = iniciar;
        objeto.ApplyModifiedProperties();
        EditorUtility.SetDirty(general);
        return true;
    }

    private static GameObject[] BuscarSimuladores()
    {
        return Object.FindObjectsByType<GameObject>(
                FindObjectsInactive.Include, FindObjectsSortMode.None)
            .Where(go => go.scene.IsValid() && go.scene.isLoaded &&
                (go.name == "XR Device Simulator" || go.name == "XR Interaction Simulator"))
            .ToArray();
    }

    private static void ConfigurarSimuladores(bool activar)
    {
        var encontrados = BuscarSimuladores();

        if (activar && encontrados.Length == 0)
        {
            // Primero el prefab CLASICO que estas utilizando en NASCOR.
            foreach (string nombre in new[] { "XR Device Simulator", "XR Interaction Simulator" })
            {
                foreach (string guid in AssetDatabase.FindAssets(nombre + " t:Prefab"))
                {
                    string ruta = AssetDatabase.GUIDToAssetPath(guid);
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(ruta);
                    if (prefab == null || prefab.name != nombre) continue;

                    GameObject nuevo = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    if (nuevo != null)
                    {
                        Undo.RegisterCreatedObjectUndo(nuevo, "Crear simulador XR");
                        encontrados = new[] { nuevo };
                    }
                    break;
                }
                if (encontrados.Length > 0) break;
            }
        }

        if (activar && encontrados.Length == 0)
        {
            Debug.LogWarning("No se ha encontrado el prefab XR Device Simulator. " +
                             "Importa el sample XR Device Simulator desde Package Manager.");
            return;
        }

        for (int i = 0; i < encontrados.Length; i++)
        {
            GameObject go = encontrados[i];
            // Si hay varios simuladores, usar uno solo para evitar entradas duplicadas.
            bool habilitarEste = activar && i == 0;
            if (go.activeSelf == habilitarEste) continue;
            Undo.RecordObject(go, "Alternar simulador XR");
            go.SetActive(habilitarEste);
            EditorSceneManager.MarkSceneDirty(go.scene);
        }
    }

    private static void GuardarCambios(XRGeneralSettings general, XRManagerSettings manager)
    {
        EditorUtility.SetDirty(general);
        EditorUtility.SetDirty(manager);
        AssetDatabase.SaveAssets();
        // La escena queda marcada si cambia el simulador. Guardarla con Ctrl+S.
    }
}
