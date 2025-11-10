using UnityEngine;

public class EnemyRealizationTargetState : EnemyBaseState
{
    private EHeardSoundCategory _category;
    private float _intensity;

    private float _time;

    private float _toTime;

    public override void EnterState(EnemyStateManager stateManager)
    {
        _category = stateManager.SoundCategory;
        _intensity = stateManager.SoundIntensity;

        stateManager.m_EnemyNavigationSystem.StopNavigating();

        _time = 0;

        stateManager.isSoundDetected = true;

        CalculateTime();
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        _time += Time.deltaTime;

        if(_time >= _toTime)
        {
            if(_intensity > 0.4)
            {
                stateManager.SwitchState(stateManager.chaseTargetState);
            }
            else if(_intensity < 0.4)
            {
                stateManager.SwitchState(stateManager.idleState);
            }
        }
    }

    private void CalculateTime()
    {
        if (_intensity < 1.0)
            _toTime = 0.5f;
        else if (_intensity < 0.1)
            _toTime = 4.0f;
    }
}
