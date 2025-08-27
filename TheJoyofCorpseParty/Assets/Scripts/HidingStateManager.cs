using UnityEngine;
using UnityEngine.InputSystem;

public class HidingStateManager : MonoBehaviour
{
    HidingBaseState currentState;
    public HidingIdleState idleState { get; private set; }
    public HidingHideState hideState { get; private set; }

    [Header("Keybinds")]
    [SerializeField] private InputActionReference m_UseKey;

    [Header("References")]
    [SerializeField]
    private Transform playerCam;

    [SerializeField]
    private GameObject player;

    public Transform playerCamInfo { get {  return playerCam; } }

    public GameObject playerInfo { get { return player; } }

    [SerializeField]
    private AnimationCurve curveToHideCamPos;

    public AnimationCurve curveToShowCamPos { get { return curveToHideCamPos; } }

    [HideInInspector]
    public Vector3 camPos;

    private void Awake()
    {
        idleState = new HidingIdleState();
        hideState = new HidingHideState();
    }

    private void OnEnable()
    {
        m_UseKey.action.started += UseAction_performed;
    }

    private void UseAction_performed(InputAction.CallbackContext obj)
    {
        currentState.EnterKey(this);
    }

    private void OnDisable()
    {
        m_UseKey.action.started -= UseAction_performed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = idleState;
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
