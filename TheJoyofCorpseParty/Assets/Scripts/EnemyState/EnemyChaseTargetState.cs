using UnityEngine;

public class EnemyChaseTargetState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.m_Enemy_Detection_System.angle = stateManager.m_ChaseAngleField;
        stateManager.m_Enemy_Navigation_System.SetRandomDestination(stateManager.m_RunSpeed,
        stateManager.m_RunAngularSpeed,
        stateManager.m_RunAcceleration);
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        
    }
}
