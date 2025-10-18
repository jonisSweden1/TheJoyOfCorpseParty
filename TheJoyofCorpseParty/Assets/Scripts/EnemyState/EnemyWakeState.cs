using UnityEngine;

public class EnemyWakeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager stateManager)
    {
        stateManager.m_Animator.SetTrigger("Awake");
        stateManager.EyesAnimators.TurnOnLights();
    }

    public override void ExitState(EnemyStateManager stateManager)
    {
        stateManager.agentMovement.enabled = true;
        stateManager.IsAwake = true;
    }

    public override void OnTriggerEnterState(Collider other, EnemyStateManager stateManager)
    {
        
    }

    public override void UpdateState(EnemyStateManager stateManager)
    {
        if(stateManager.m_Animator)
        {
            if (stateManager.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                stateManager.SwitchState(stateManager.idleState);
            }
        }
    }
}
