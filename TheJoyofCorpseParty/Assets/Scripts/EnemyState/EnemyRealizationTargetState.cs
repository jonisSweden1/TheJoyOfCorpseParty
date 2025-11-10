using UnityEngine;

public class EnemyRealizationTargetState : EnemyBaseState
{
    private EHeardSoundCategory _category;
    private float _intensity;

    private float _time;

    private float _toTime;

    public override void EnterState(EnemyStateManager stateManager)
    {
        if(!stateManager.isSoundDetected)
        {
            _category = stateManager.SoundCategory;
            _intensity = stateManager.SoundIntensity;
        }
        else
        {
            _category = EHeardSoundCategory.None;
            _intensity = 0.0f;
        }

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
            if(_intensity > 0.41)
            {
                stateManager.SwitchState(stateManager.chaseTargetState);
            }
            else if(_intensity < 0.4)
            {
                stateManager.SwitchState(stateManager.idleState);
            }
            else if(_intensity < 0.0)
            {

            }
        }
    }

    private void CalculateTime()
    {
        if (_intensity > 0.31)
            _toTime = 0.5f;
        else if (_intensity < 0.3)
            _toTime = 4.0f;
        else if (_intensity < 0.0)
            _toTime = 0.0f;
    }
}
