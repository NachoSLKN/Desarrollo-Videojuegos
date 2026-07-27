using System.IO;
using UnityEditor;
using UnityEngine;

public static class IronGiantDesignTools
{
    private const string DesignFolder = "Assets/Design";

    [MenuItem("Iron Giant/Design/Open Design Folder")]
    public static void OpenDesignFolder()
    {
        string absolutePath = GetAbsolutePath(DesignFolder);

        if (!Directory.Exists(absolutePath))
        {
            Debug.LogError($"Design folder not found: {absolutePath}");
            return;
        }

        EditorUtility.RevealInFinder(absolutePath);
    }

    [MenuItem("Iron Giant/Design/Open GDD")]
    public static void OpenGDD()
    {
        OpenDocument("GDD.md");
    }

    [MenuItem("Iron Giant/Design/Open Roadmap")]
    public static void OpenRoadmap()
    {
        OpenDocument("Roadmap.md");
    }

    [MenuItem("Iron Giant/Design/Open Todo")]
    public static void OpenTodo()
    {
        OpenDocument("Todo.md");
    }

    [MenuItem("Iron Giant/Design/Open Technical Design")]
    public static void OpenTechnicalDesign()
    {
        OpenDocument("TechnicalDesign.md");
    }

    [MenuItem("Iron Giant/Design/Open Animation List")]
    public static void OpenAnimationList()
    {
        OpenDocument("AnimationList.md");
    }

    [MenuItem("Iron Giant/Design/Open Art Direction")]
    public static void OpenArtDirection()
    {
        OpenDocument("ArtDirection.md");
    }

    [MenuItem("Iron Giant/Design/Open Asset Plan")]
    public static void OpenAssetPlan()
    {
        OpenDocument("AssetPlan.md");
    }

    [MenuItem("Iron Giant/Design/Open Changelog")]
    public static void OpenChangelog()
    {
        OpenDocument("Changelog.md");
    }

    [MenuItem("Iron Giant/Design/Open Controls")]
    public static void OpenControls()
    {
        OpenDocument("Controls.md");
    }

    [MenuItem("Iron Giant/Design/Open Devlog Plan")]
    public static void OpenDevlogPlan()
    {
        OpenDocument("DevlogPlan.md");
    }

    [MenuItem("Iron Giant/Design/Open Enemy List")]
    public static void OpenEnemyList()
    {
        OpenDocument("EnemyList.md");
    }

    [MenuItem("Iron Giant/Design/Open Ideas")]
    public static void OpenIdeas()
    {
        OpenDocument("Ideas.md");
    }

    [MenuItem("Iron Giant/Design/Open Known Issues")]
    public static void OpenKnownIssues()
    {
        OpenDocument("KnownIssues.md");
    }

    [MenuItem("Iron Giant/Design/Open README")]
    public static void OpenReadme()
    {
        OpenDocument("README.md");
    }

    [MenuItem("Iron Giant/Design/Open Scene Setup")]
    public static void OpenSceneSetup()
    {
        OpenDocument("SceneSetup.md");
    }

    [MenuItem("Iron Giant/Design/Open Story")]
    public static void OpenStory()
    {
        OpenDocument("Story.md");
    }

    private static void OpenDocument(string fileName)
    {
        string relativePath = Path.Combine(DesignFolder, fileName);
        string absolutePath = GetAbsolutePath(relativePath);

        if (!File.Exists(absolutePath))
        {
            Debug.LogWarning($"Document not found: {relativePath}");

            string designFolderPath = GetAbsolutePath(DesignFolder);

            if (Directory.Exists(designFolderPath))
            {
                EditorUtility.RevealInFinder(designFolderPath);
            }

            return;
        }

        EditorUtility.OpenWithDefaultApp(absolutePath);
    }

    private static string GetAbsolutePath(string relativePath)
    {
        string projectRoot = Directory.GetParent(Application.dataPath)?.FullName;

        if (string.IsNullOrWhiteSpace(projectRoot))
        {
            return relativePath;
        }

        return Path.GetFullPath(Path.Combine(projectRoot, relativePath));
    }
}