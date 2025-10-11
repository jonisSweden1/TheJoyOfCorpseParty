using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyBaseState
{
    //private int rndNumber;

    public override void EnterState(EnemyStateManager stateManager)
    {
        /*
        rndNumber = Random.Range(0, 100);
        
        if(rndNumber < 30)
        {
            stateManager.m_Enemy_Detection_System.angle = stateManager.m_ChaseAngleField;
            stateManager.m_Enemy_Navigation_System.SetRandomDestination(stateManager.m_RunSpeed,
            stateManager.m_RunAngularSpeed,
            stateManager.m_RunAcceleration);
        }
        else
        {
            stateManager.m_Enemy_Detection_System.angle = stateManager.m_RoamAngleField;
            stateManager.m_Enemy_Navigation_System.SetRandomDestination(stateManager.m_WalkSpeed,
            stateManager.m_WalkAngularSpeed,
            stateManager.m_WalkAcceleration);
        }
        */
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
