using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FixPivot : MonoBehaviour
{
    [MenuItem("Tools/Fix Pivot (Center All) %#p")]
    static void tryFixPivot()
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected.");
            return;
        }

        foreach (GameObject go in Selection.gameObjects)
        {
            var meshFilters = go.GetComponentsInChildren<MeshFilter>();

            if (meshFilters.Length == 0)
            {
                Debug.LogWarning(go.name + " has no MeshFilter!");
                continue;
            }

            // Calculate average center of all child meshes
            Vector3 avg = Vector3.zero;
            int count = 0;
            foreach (var mf in meshFilters)
            {
                avg += mf.sharedMesh.bounds.center;
                count++;
            }
            avg /= count;

            Undo.RecordObject(go.transform, "Fix Pivot");
            go.transform.position += go.transform.TransformVector(avg);
            Debug.Log($"Pivot centered for {go.name}");
        }

        Debug.Log(" All selected pivots centered!");
    }
}