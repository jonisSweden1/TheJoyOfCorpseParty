using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyDetectionSystem))]
public class EnemyDetectionSystemEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyDetectionSystem _eds = (EnemyDetectionSystem)target;

        if(_eds.headPosition == null)
        {
            DrawOnFeet(_eds);
        }
        else
        {
            DrawInHead(_eds);
        }
    }
    
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void DrawInHead(EnemyDetectionSystem eds)
    {
        Handles.color = Color.white;
        Handles.DrawWireArc(eds.headPosition.position, Vector3.up, Vector3.forward, 360, eds.radius);

        Vector3 viewAngle01 = DirectionFromAngle(eds.headPosition.eulerAngles.y, -eds.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(eds.headPosition.eulerAngles.y, eds.angle / 2);

        Handles.color = Color.yellow;
        Vector3 enemyPosition = eds.headPosition.position;

        Handles.DrawLine(enemyPosition, enemyPosition + viewAngle01 * eds.radius);
        Handles.DrawLine(enemyPosition, enemyPosition + viewAngle02 * eds.radius);

        if (eds.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(eds.headPosition.position, eds.PlayerRef.transform.position);
        }
    }

    private void DrawOnFeet(EnemyDetectionSystem eds)
    {
        Handles.color = Color.white;
        Handles.DrawWireArc(eds.transform.position, Vector3.up, Vector3.forward, 360, eds.radius);

        Vector3 viewAngle01 = DirectionFromAngle(eds.transform.eulerAngles.y, -eds.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(eds.transform.eulerAngles.y, eds.angle / 2);

        Handles.color = Color.yellow;
        Vector3 enemyPosition = eds.transform.position;

        Handles.DrawLine(enemyPosition, enemyPosition + viewAngle01 * eds.radius);
        Handles.DrawLine(enemyPosition, enemyPosition + viewAngle02 * eds.radius);

        if (eds.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(eds.transform.position, eds.PlayerRef.transform.position);
        }
    }
}
