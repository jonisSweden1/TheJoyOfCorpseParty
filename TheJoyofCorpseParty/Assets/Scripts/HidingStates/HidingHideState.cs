using UnityEngine;

public class HidingHideState : HidingBaseState
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private float currentTime = 0;
    private float currentCurveEvaluation;

    public override void EnterKey(HidingStateManager stateManager)
    {
        stateManager.playerCamInfo.GetComponent<PlayerHidingHandler>().enabled = true;
        stateManager.ChangeState(stateManager.idleState);
    }

    public override void EnterState(HidingStateManager stateManager)
    {
        startPosition = stateManager.playerCamInfo.position;
        startRotation = stateManager.playerCamInfo.rotation;

        currentTime = 0;
    }

    public override void ExitState(HidingStateManager stateManager)
    {
        
    }

    public override void UpdateState(HidingStateManager stateManager)
    {
        if(currentTime <= 1)
        {
            currentTime += Time.deltaTime;
            currentCurveEvaluation = stateManager.curveToShowCamPos.Evaluate(currentTime);

            TransitionCamera(stateManager);
        }
    }

    private void TransitionCamera(HidingStateManager stateManager)
    {
        stateManager.playerCamInfo.position = Vector3.Lerp(startPosition, stateManager.camTrans.position, currentCurveEvaluation);
        stateManager.playerCamInfo.rotation = Quaternion.Lerp(startRotation, stateManager.camTrans.rotation, currentCurveEvaluation);
    }
}
