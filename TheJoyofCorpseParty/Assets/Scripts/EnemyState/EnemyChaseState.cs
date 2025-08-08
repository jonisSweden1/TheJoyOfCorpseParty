using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.m_Agent.acceleration = stateManager.m_RunAcceleration;
        stateManager.m_Agent.speed = stateManager.m_RunSpeed;
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
            Vector3 playerPosition = stateManager.m_Enemy_Detection_System.PlayerRef.transform.position;
            stateManager.m_Agent.SetDestination(playerPosition);
        }
        else if(stateManager.m_Agent.pathStatus == NavMeshPathStatus.PathComplete && stateManager.m_Agent.remainingDistance <= stateManager.m_Agent.stoppingDistance)
        {
            stateManager.SwitchState(stateManager.seekState);
        }
    }
}
