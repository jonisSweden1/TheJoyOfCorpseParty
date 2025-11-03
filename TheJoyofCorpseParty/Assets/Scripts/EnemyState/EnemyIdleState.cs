using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float _time = 0f;
    private float _idleTime = 0f;

    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.isPlayerDetected = false;

        stateManager.m_EnemyVisionDetection.angle = stateManager.m_RoamAngleField;

        _idleTime = stateManager.m_TimeToRoam;
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        _time = 0f;
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        _time += Time.deltaTime;

        if(_time >= _idleTime)
        {
            if(!stateManager.m_EnemyNavigationSystem.CheckListDestinationsNotNull())
            {
                Debug.LogError("There is no destination, so it will not start roaming");
                return;
            }

            stateManager.SwitchState(stateManager.roamState);
        }
    }
}
