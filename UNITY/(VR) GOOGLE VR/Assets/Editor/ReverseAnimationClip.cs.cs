using UnityEngine;
using UnityEditor;

public class ReverseAnimationClip
{
    [MenuItem("Tools/Animation/Reverse Selected Clip")]
    static void ReverseSelectedClip()
    {
        AnimationClip originalClip = Selection.activeObject as AnimationClip;

        if (originalClip == null)
        {
            Debug.LogError("Select an AnimationClip in the Project window.");
            return;
        }

        string originalPath = AssetDatabase.GetAssetPath(originalClip);
        string directory = System.IO.Path.GetDirectoryName(originalPath);
        string fileName = System.IO.Path.GetFileNameWithoutExtension(originalPath);

        AnimationClip reversedClip = new AnimationClip();
        reversedClip.frameRate = originalClip.frameRate;

        EditorCurveBinding[] bindings =
            AnimationUtility.GetCurveBindings(originalClip);

        foreach (EditorCurveBinding binding in bindings)
        {
            AnimationCurve originalCurve =
                AnimationUtility.GetEditorCurve(originalClip, binding);

            Keyframe[] keys = originalCurve.keys;
            Keyframe[] reversedKeys = new Keyframe[keys.Length];

            float length = originalClip.length;

            for (int i = 0; i < keys.Length; i++)
            {
                Keyframe oldKey = keys[i];

                Keyframe newKey = new Keyframe(
                    length - oldKey.time,
                    oldKey.value,
                    -oldKey.outTangent,
                    -oldKey.inTangent
                );

                reversedKeys[keys.Length - 1 - i] = newKey;
            }

            AnimationCurve reversedCurve =
                new AnimationCurve(reversedKeys);

            AnimationUtility.SetEditorCurve(
                reversedClip,
                binding,
                reversedCurve
            );
        }

        string newPath =
            directory + "/" + fileName + "_Reversed.anim";

        AssetDatabase.CreateAsset(reversedClip, newPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Reversed animation created: " + newPath);
    }
}