using UnityEditor;
using UnityEngine;

/// <summary>
/// Single entry point to the online GDD. Set its URL once using the menu.
/// </summary>
public static class IronGiantDesignTools
{
    private const string UrlPreference = "IronGiant.GDD.Url";

    [MenuItem("Iron Giant/Design/Open GDD Online", priority = 10)]
    public static void OpenGDD()
    {
        string url = EditorPrefs.GetString(UrlPreference, "");
        if (string.IsNullOrWhiteSpace(url))
        {
            ConfigureGDDUrl();
            return;
        }

        Application.OpenURL(url);
    }

    [MenuItem("Iron Giant/Design/Configure GDD URL", priority = 11)]
    public static void ConfigureGDDUrl()
    {
        GDDUrlWindow.Open();
    }

    private sealed class GDDUrlWindow : EditorWindow
    {
        private string url = "";

        public static void Open()
        {
            GDDUrlWindow window = GetWindow<GDDUrlWindow>(true, "Iron Giant — GDD URL");
            window.minSize = new Vector2(440, 125);
            window.url = EditorPrefs.GetString(UrlPreference, "");
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("URL del GDD publicado en GitHub Pages", EditorStyles.boldLabel);
            url = EditorGUILayout.TextField("URL", url);
            EditorGUILayout.HelpBox("Introduce la URL real del GDD del Gigante. No la web de Grid Studio.", MessageType.Info);

            if (GUILayout.Button("Guardar URL"))
            {
                string clean = url.Trim();
                if (!System.Uri.TryCreate(clean, System.UriKind.Absolute, out System.Uri parsed)
                    || (parsed.Scheme != System.Uri.UriSchemeHttps && parsed.Scheme != System.Uri.UriSchemeHttp))
                {
                    EditorUtility.DisplayDialog("URL no valida", "Introduce una URL http:// o https:// valida.", "OK");
                    return;
                }

                EditorPrefs.SetString(UrlPreference, clean);
                Close();
            }
        }
    }
}
