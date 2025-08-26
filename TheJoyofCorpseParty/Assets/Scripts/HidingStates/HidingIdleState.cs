using UnityEngine;

public class HidingIdleState : HidingBaseState
{
    public override void EnterKey(HidingStateManager stateManager)
    {
        PlayerHidingHandler handler = stateManager.playerCamInfo.gameObject.GetComponent<PlayerHidingHandler>();
        if (handler != null)
        {
            Vector3 pos = Vector3.zero;

            if (handler.FindHidingSpot(out pos))
            {
                stateManager.camPos = pos;
                stateManager.ChangeState(stateManager.hideState);
            }
        }
    }

    public override void EnterState(HidingStateManager stateManager)
    {
        stateManager.playerCamInfo.localPosition = Vector3.zero;
        stateManager.playerCamInfo.GetComponent<PlayerCameraController>().enabled = true;
        stateManager.playerInfo.SetActive(true);
    }

    public override void ExitState(HidingStateManager stateManager)
    {
        stateManager.playerCamInfo.GetComponent<PlayerCameraController>().enabled = false;
        stateManager.playerInfo.SetActive(false);
    }

    public override void UpdateState(HidingStateManager stateManager)
    {

    }
}
