using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySoundDetection))]
public class EnemySoundDetectionEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemySoundDetection esd = (EnemySoundDetection)target;

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(esd.HeadTransform.position, Vector3.up, esd.HearingRange);
    }
}
