using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShowAllDestinationsDebug))]
public class ShowAllDestinationsDebugEditor : Editor
{
    private void OnSceneGUI()
    {
        Handles.color = Color.green;

        ShowAllDestinationsDebug debugScript = (ShowAllDestinationsDebug)target;

        Transform t = debugScript.transform;
        
        foreach(Transform child in t)
        {
            Handles.DrawWireDisc(child.position, Vector3.down, 1);
            Handles.Label(child.position + Vector3.up * 1, child.name);
        }
    }
}
