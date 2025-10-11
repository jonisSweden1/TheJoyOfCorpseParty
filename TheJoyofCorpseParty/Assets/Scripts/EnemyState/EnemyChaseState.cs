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
        if(stateManager.m_Enemy_Detection_System.CanSeePlayer)
        {
            Transform playerTransform = stateManager.m_Enemy_Detection_System.PlayerRef.transform;
            stateManager.m_Enemy_Navigation_System.SetDestination(playerTransform, 
                stateManager.m_RunSpeed, 
                stateManager.m_RunAngularSpeed, 
                stateManager.m_RunAcceleration);
        }
        else if(stateManager.m_Enemy_Navigation_System.CheckNavigationFinished())
        {
            stateManager.SwitchState(stateManager.realizationPlayerState);
        }
    }
}
