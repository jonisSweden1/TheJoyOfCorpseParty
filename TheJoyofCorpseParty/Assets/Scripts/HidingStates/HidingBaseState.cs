using UnityEngine;

public abstract class HidingBaseState
{
    public abstract void EnterState(HidingStateManager stateManager);

    public abstract void ExitState(HidingStateManager stateManager);

    public abstract void UpdateState(HidingStateManager stateManager);
}
