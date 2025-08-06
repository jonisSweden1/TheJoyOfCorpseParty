using UnityEngine;

public class EnemySeekState : EnemyBaseState
{
    private float time = 0;
    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.m_Agent.ResetPath();

        Debug.Log("Seeking");
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        time = 0;
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        if(stateManager.m_Enemy_Detection_System.CanSeePlayer)
        {
            time += Time.deltaTime;

            if (time >= stateManager.m_ExposureTime)
                stateManager.SwitchState(stateManager.chaseState);
        }
        else if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            stateManager.SwitchState(stateManager.idleState);
        }
    }
}
