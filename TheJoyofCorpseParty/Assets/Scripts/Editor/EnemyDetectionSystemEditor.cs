using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyDetectionSystem))]
public class EnemyDetectionSystemEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyDetectionSystem _eds = (EnemyDetectionSystem)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(_eds.transform.position, Vector3.up, Vector3.forward, 360, _eds.radius);

        Vector3 viewAngle01 = DirectionFromAngle(_eds.transform.eulerAngles.y, -_eds.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(_eds.transform.eulerAngles.y, _eds.angle / 2);

        Handles.color = Color.yellow;
        Vector3 enemyPosition = _eds.transform.position;

        Handles.DrawLine(enemyPosition, enemyPosition + viewAngle01 * _eds.radius);
        Handles.DrawLine(enemyPosition, enemyPosition + viewAngle02 * _eds.radius);

        if(_eds.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(_eds.transform.position, _eds.PlayerRef.transform.position);
        }
    }
    
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
