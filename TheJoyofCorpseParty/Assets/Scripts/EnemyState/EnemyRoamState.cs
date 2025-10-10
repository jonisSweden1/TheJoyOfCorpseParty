using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.m_Enemy_Detection_System.angle = stateManager.m_RoamAngleField;

        stateManager.m_Enemy_Navigation_System.SetRandomDestination(stateManager.m_WalkSpeed,
            stateManager.m_WalkAngularSpeed,
            stateManager.m_WalkAcceleration);
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        if(stateManager.m_Enemy_Navigation_System.CheckNavigationFinished())
        {
            stateManager.SwitchState(stateManager.idleState);
        }
    }
}
