using UnityEngine;

public class EnemyRealizationPlayerState : EnemyBaseState
{
    private float time = 0;
    private float savedTime = 0;
    public override void EnterState(EnemyStateManager stateManager)
    {
        if(savedTime > 0)
        {
            time = savedTime;
        }

        stateManager.m_EnemyNavigationSystem.StopNavigating();

        Debug.Log("Seeking");
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        time = 0;
        stateManager.isPlayerDetected = false;
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        if(stateManager.m_EnemyVisionDetection.CanSeePlayer)
        {
            stateManager.isPlayerDetected = true;

            time += Time.deltaTime;

            if (time >= stateManager.m_ExposureTime)
            {
                savedTime = time;
                stateManager.SwitchState(stateManager.chasePlayerState);
            }
        }
        else if(time > 0)
        {
            stateManager.isPlayerDetected = false;

            time -= Time.deltaTime;
        }
        else
        {
            savedTime = 0;
            stateManager.SwitchState(stateManager.idleState);
        }
    }
}
