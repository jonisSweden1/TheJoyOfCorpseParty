using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyBaseState
{
    int previousDestinationIndex = -1;

    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.m_Agent.speed = stateManager.m_WalkSpeed;
        stateManager.m_Agent.angularSpeed = stateManager.m_WalkAngularSpeed;
        stateManager.m_Agent.acceleration = stateManager.m_WalkAcceleration;

        stateManager.m_Enemy_Detection_System.angle = stateManager.m_RoamAngleField;

        int rndIndex = Random.Range(0, stateManager.Destinations.Length - 1);

        if(previousDestinationIndex == rndIndex && previousDestinationIndex != -1)
        {
            if(rndIndex > stateManager.Destinations.Length - 1)
            {
                rndIndex--;
            }
            else
            {
                rndIndex++;
            }
        }

        Debug.Log(rndIndex);

        stateManager.m_Agent.SetDestination(stateManager.Destinations[rndIndex].position);

        previousDestinationIndex = rndIndex;
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        if(stateManager.m_Agent.pathStatus == NavMeshPathStatus.PathComplete && stateManager.m_Agent.remainingDistance <= stateManager.m_Agent.stoppingDistance)
        {
            stateManager.SwitchState(stateManager.idleState);
        }
    }
}
