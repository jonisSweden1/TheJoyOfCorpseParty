using System;
using UnityEngine;

public class HidingStateManager : MonoBehaviour
{
    HidingBaseState currentState;
    public HidingIdleState idleState;
    public HidingHideState hideState;

    private void Awake()
    {
        idleState = new HidingIdleState();
        hideState = new HidingHideState();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(HidingBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
