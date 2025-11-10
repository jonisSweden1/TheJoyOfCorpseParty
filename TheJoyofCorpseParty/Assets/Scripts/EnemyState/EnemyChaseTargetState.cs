using UnityEngine;

public class EnemyChaseTargetState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.m_EnemyVisionDetection.angle = stateManager.m_ChaseAngleField;
        stateManager.m_EnemyNavigationSystem.SetDestination(stateManager.DetectionPosition, stateManager.m_RunSpeed,
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
        if (stateManager.m_EnemyNavigationSystem.CheckNavigationFinished())
            stateManager.SwitchState(stateManager.realizationTargetState);
    }
}
