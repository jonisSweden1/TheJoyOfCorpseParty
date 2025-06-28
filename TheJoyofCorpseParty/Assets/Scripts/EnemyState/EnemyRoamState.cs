using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.mAgent.speed = stateManager.m_WalkSpeed;
        stateManager.mAgent.acceleration = stateManager.m_WalkAcceleration;

        int rndIndex = Random.Range(0, stateManager.Destinations.Length - 1);

        stateManager.mAgent.SetDestination(stateManager.Destinations[rndIndex].position);
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        if(stateManager.mAgent.pathStatus == NavMeshPathStatus.PathComplete && stateManager.mAgent.remainingDistance <= stateManager.mAgent.stoppingDistance)
        {
            stateManager.SwitchState(stateManager.idleState);
        }

        // Update 0.2.0 TODO: Check area if the player is on sight.
        // Make it stop and wait when it detects the player, and then switch the state into "Chase"
    }
}
