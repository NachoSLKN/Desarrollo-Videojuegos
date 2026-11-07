using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Preview-first cleanup for the Iron Giant Unity project.
/// All moves go through AssetDatabase.MoveAsset to keep .meta GUIDs intact.
/// No deletion is performed automatically.
/// </summary>
public static class IronGiantProjectOrganizer
{
    private struct Move
    {
        public readonly string From;
        public readonly string To;
        public Move(string from, string to) { From = from; To = to; }
    }

    // Only targeted, known script names. Everything else is left untouched.
    // Add more entries after reviewing the preview and checking string-based paths.
    private static readonly Move[] ScriptMoves =
    {
        new Move("Assets/Scripts/IronGiantWarArmToggle.cs", "Assets/Scripts/Characters/IronGiant/IronGiantWarArmToggle.cs"),
        new Move("Assets/Scripts/IronGiantWarMode.cs", "Assets/Scripts/Characters/IronGiant/IronGiantWarMode.cs"),
        new Move("Assets/Scripts/PlayerVariantLoader.cs", "Assets/Scripts/Characters/PlayerVariantLoader.cs"),
        new Move("Assets/Scripts/ScrapPickup.cs", "Assets/Scripts/Gameplay/ScrapPickup.cs"),
        new Move("Assets/Scripts/ScrapReward.cs", "Assets/Scripts/Gameplay/ScrapReward.cs"),
        new Move("Assets/Scripts/Managers/DeanShopUI.cs", "Assets/Scripts/UI/DeanShopUI.cs"),
        new Move("Assets/Scripts/Combat/LockOnIndicatorUI.cs", "Assets/Scripts/UI/LockOnIndicatorUI.cs"),
        new Move("Assets/Scripts/Utilities/CharacterSwitcher.cs", "Assets/Scripts/Characters/CharacterSwitcher.cs"),
        new Move("Assets/Scripts/Utilities/CharacterSwitchInput.cs", "Assets/Scripts/Characters/CharacterSwitchInput.cs"),
        new Move("Assets/Scripts/SceneTransition.cs", "Assets/Scripts/Core/SceneTransition.cs"),
        new Move("Assets/Scripts/ComicLoadingScreen.cs", "Assets/Scripts/UI/ComicLoadingScreen.cs"),
        new Move("Assets/Scripts/TitleScreenController.cs", "Assets/Scripts/UI/TitleScreenController.cs"),
    };

    private static readonly string[] RemovalCandidates =
    {
        "Assets/PolygonCity",
        "Assets/_Recovery"
    };

    [MenuItem("Iron Giant/Project/1. Preview Script Reorganization", priority = 100)]
    private static void Preview()
    {
        List<Move> valid = GetMoves(out List<string> warnings);
        Debug.Log("[Iron Giant] SCRIPT MOVE PREVIEW — " + valid.Count + " moves (no files changed)\n"
            + string.Join("\n", valid.ConvertAll(m => m.From + "  ->  " + m.To))
            + (warnings.Count == 0 ? "" : "\nWarnings:\n" + string.Join("\n", warnings)));
        EditorUtility.DisplayDialog("Preview finished", "Review the Console for the move plan and warnings. No files were changed.", "OK");
    }

    [MenuItem("Iron Giant/Project/2. Apply Script Reorganization", priority = 101)]
    private static void Apply()
    {
        List<Move> valid = GetMoves(out List<string> warnings);
        if (warnings.Count > 0)
        {
            Debug.LogWarning("[Iron Giant] Reorganization cancelled because of conflicts:\n" + string.Join("\n", warnings));
            EditorUtility.DisplayDialog("Reorganization cancelled", "There are conflicts. Read the Console before proceeding.", "OK");
            return;
        }
        if (valid.Count == 0)
        {
            EditorUtility.DisplayDialog("Nothing to move", "No pending moves were found.", "OK");
            return;
        }
        if (!EditorUtility.DisplayDialog("Confirm script moves", "Commit your current project to Git first. Move " + valid.Count + " scripts using Unity AssetDatabase?", "Move scripts", "Cancel"))
            return;

        int succeeded = 0;
        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (Move move in valid)
            {
                string folder = Path.GetDirectoryName(move.To).Replace('\\', '/');
                EnsureFolder(folder);
                string error = AssetDatabase.MoveAsset(move.From, move.To);
                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError("[Iron Giant] Could not move " + move.From + ": " + error);
                    break;
                }
                succeeded++;
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }
        Debug.Log("[Iron Giant] Finished " + succeeded + "/" + valid.Count + " moves. Check Console, compilation, and play mode.");
    }

    [MenuItem("Iron Giant/Project/3. Inspect Removal Candidates", priority = 102)]
    private static void InspectDeletion()
    {
        foreach (string folder in RemovalCandidates)
        {
            if (!AssetDatabase.IsValidFolder(folder))
            {
                Debug.Log("[Iron Giant] Not present: " + folder);
                continue;
            }
            string[] guids = AssetDatabase.FindAssets("", new[] { folder });
            Debug.Log("[Iron Giant] Candidate " + folder + ": " + guids.Length
                + " indexed assets. IMPORTANT: this does not prove whether scenes/prefabs depend on them.");
        }
    }

    [MenuItem("Iron Giant/Project/4. Delete PolygonCity and Recovery (CONFIRM)", priority = 103)]
    private static void DeleteCandidates()
    {
        if (!EditorUtility.DisplayDialog("Delete imported/recovery assets?",
            "This deletes Assets/PolygonCity and Assets/_Recovery IF present.\n\n"
            + "First commit to Git, inspect scenes/prefabs for missing references, and confirm no shaders, materials, scripts, or prefabs depend on PolygonCity.\n\n"
            + "Unity will remove their .meta files too. This operation is not undoable through Ctrl+Z.",
            "Continue", "Cancel"))
            return;

        if (!EditorUtility.DisplayDialog("FINAL CONFIRMATION", "Permanently delete PolygonCity and _Recovery from Assets now?", "Delete", "Cancel"))
            return;

        foreach (string folder in RemovalCandidates)
        {
            if (!AssetDatabase.IsValidFolder(folder))
                continue;
            if (AssetDatabase.DeleteAsset(folder))
                Debug.Log("[Iron Giant] Deleted " + folder);
            else
                Debug.LogError("[Iron Giant] Failed to delete " + folder);
        }
        AssetDatabase.Refresh();
    }

    private static List<Move> GetMoves(out List<string> warnings)
    {
        warnings = new List<string>();
        List<Move> valid = new List<Move>();
        foreach (Move m in ScriptMoves)
        {
            bool sourceExists = AssetDatabase.LoadMainAssetAtPath(m.From) != null;
            bool destExists = AssetDatabase.LoadMainAssetAtPath(m.To) != null;
            if (sourceExists && destExists)
                warnings.Add("Destination already occupied: " + m.To);
            else if (sourceExists)
                valid.Add(m);
        }
        return valid;
    }

    private static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path)) return;
        string parent = Path.GetDirectoryName(path).Replace('\\', '/');
        if (!AssetDatabase.IsValidFolder(parent)) EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, Path.GetFileName(path));
    }
}
