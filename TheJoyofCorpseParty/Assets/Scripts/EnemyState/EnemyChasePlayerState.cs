using UnityEngine;
using UnityEngine.AI;

public class EnemyChasePlayerState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager stateManager)
    {
        //Debug.Log("CHASE!!!");
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        if(stateManager.m_EnemyVisionDetection.CanSeePlayer)
        {
            Transform playerTransform = stateManager.m_EnemyVisionDetection.PlayerRef.transform;
            stateManager.m_EnemyNavigationSystem.SetDestination(playerTransform, 
                stateManager.m_RunSpeed, 
                stateManager.m_RunAngularSpeed, 
                stateManager.m_RunAcceleration);
        }
        else if(stateManager.m_EnemyNavigationSystem.CheckNavigationFinished())
        {
            stateManager.SwitchState(stateManager.realizationPlayerState);
        }
    }
}
