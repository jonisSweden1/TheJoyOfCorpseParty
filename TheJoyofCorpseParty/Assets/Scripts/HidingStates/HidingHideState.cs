using UnityEngine;

public class HidingHideState : HidingBaseState
{
    public override void EnterKey(HidingStateManager stateManager)
    {
        stateManager.ChangeState(stateManager.idleState);
    }

    public override void EnterState(HidingStateManager stateManager)
    {
        
    }

    public override void ExitState(HidingStateManager stateManager)
    {
        
    }

    public override void UpdateState(HidingStateManager stateManager)
    {
        TransitionCamera();
    }

    private void TransitionCamera()
    {
        
    }
}
