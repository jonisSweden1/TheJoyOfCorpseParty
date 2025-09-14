using UnityEngine;

public class HidingHideState : HidingBaseState
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float startFieldOfView;

    private float currentTime = 0;
    private float currentCurveEvaluation;

    public override void EnterKey(HidingStateManager stateManager)
    {
        stateManager.playerCamInfo.GetComponent<PlayerHidingHandler>().enabled = true;
        stateManager.ChangeState(stateManager.idleState);
    }

    public override void EnterState(HidingStateManager stateManager)
    {
        startPosition = stateManager.playerCamInfo.transform.position;
        startRotation = stateManager.playerCamInfo.transform.rotation;

        startFieldOfView = stateManager.playerCamInfo.fieldOfView;

        currentTime = 0;
    }

    public override void ExitState(HidingStateManager stateManager)
    {
        stateManager.playerCamInfo.fieldOfView = startFieldOfView;
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
        stateManager.playerCamInfo.transform.position = Vector3.Lerp(startPosition, stateManager.camInfo.CamTrans.position, currentCurveEvaluation);
        stateManager.playerCamInfo.transform.rotation = Quaternion.Lerp(startRotation, stateManager.camInfo.CamTrans.rotation, currentCurveEvaluation);
        stateManager.playerCamInfo.fieldOfView = Mathf.Lerp(startFieldOfView, stateManager.camInfo.fieldOfView, currentCurveEvaluation);
    }
}
