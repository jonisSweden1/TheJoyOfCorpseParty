using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyBaseState
{
    int previousDestinationIndex = -1;

    public override void EnterState(EnemyStateManager stateManager)
    {
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

        stateManager.m_Enemy_Navigation_System.SetDestination(stateManager.Destinations[rndIndex],
            stateManager.m_WalkSpeed,
            stateManager.m_WalkAngularSpeed,
            stateManager.m_WalkAcceleration);

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
        if(stateManager.m_Enemy_Navigation_System.CheckNavigationFinished())
        {
            stateManager.SwitchState(stateManager.idleState);
        }
    }
}
