using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AgentMovement))]
[RequireComponent(typeof(EnemyVisionDetection))]
public class EnemyStateManager : MonoBehaviour
{
    public EnemyVisionDetection m_Enemy_Detection_System {  get; private set; }
    public EnemyNavigationSystem m_Enemy_Navigation_System { get; private set; }
    public AgentMovement agentMovement { get; private set; }
    public Animator m_Animator { get; private set; }

    public LightEyesAnimators EyesAnimators { 
        get { return _eyesAnimators; } 
    }

    [SerializeField]
    private LightEyesAnimators _eyesAnimators;

    public EnemySleepState sleepState;
    public EnemyWakeState wakeState;
    public EnemyIdleState idleState;
    public EnemyRoamState roamState;
    public EnemyRealizationPlayerState realizationPlayerState;
    public EnemyRealizationTargetState realizationTargetState;
    public EnemyChasePlayerState chasePlayerState;
    public EnemyChaseTargetState chaseTargetState;

    EnemyBaseState _currentState;

    [Header("Main Properties")]
    [SerializeField]
    private bool _isAwake = false;

    public bool IsAwake { get { return _isAwake; } set { _isAwake = value; } }

    [Header("Speed")]
    public float m_WalkSpeed;
    public float m_WalkAngularSpeed;

    public float m_RunSpeed;
    public float m_RunAngularSpeed;

    [Header("Acceleration")]
    public float m_WalkAcceleration;
    public float m_RunAcceleration;

    [Header("Angle change")]
    public float m_RoamAngleField;
    public float m_ChaseAngleField;

    [Header("Time To Roam (s)")]
    [Tooltip("Write it in seconds")]
    [SerializeField]
    private float m_MinTimeToRoamToNextLocation;

    [Tooltip("Write it in seconds")]
    [SerializeField]
    private float m_MaxTimeToRoamToNextLocation;

    public float m_TimeToRoam { get { return RandomizeTimeToRoam(); } }

    [Header("Exposure time (s)")]
    [Tooltip("Write it in seconds")]
    public float m_ExposureTime;

    public event Action onStateChanged;

    void Awake()
    {
        //Get components
        m_Enemy_Detection_System = GetComponent<EnemyVisionDetection>();
        m_Enemy_Navigation_System = GetComponent<EnemyNavigationSystem>();
        m_Animator = GetComponent<Animator>();
        agentMovement = GetComponent<AgentMovement>();
        

        //Initialize the states
        sleepState = new EnemySleepState();
        wakeState = new EnemyWakeState();
        idleState = new EnemyIdleState();
        roamState = new EnemyRoamState();
        realizationPlayerState = new EnemyRealizationPlayerState();
        realizationTargetState = new EnemyRealizationTargetState();
        chasePlayerState = new EnemyChasePlayerState();
        chaseTargetState = new EnemyChaseTargetState();
    }

    private void Start()
    {
        if(_isAwake)
        {
            m_Animator.Play("Idle");
            _currentState = idleState;
        }
        else
        {
            agentMovement.enabled = false;
            _currentState = sleepState;
        }

        _currentState.EnterState(this);

        if (m_MaxTimeToRoamToNextLocation < m_MinTimeToRoamToNextLocation)
        {
            Debug.LogError("Maximum time to roam is lower than minimum. It can break the randomizer");
        }
    }

    void OnEnable()
    {
        m_Enemy_Detection_System.m_OnDetected += TriggerRealization;
    }

    void OnDisable()
    {
        m_Enemy_Detection_System.m_OnDetected -= TriggerRealization;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void AwakePlayer()
    {
        if(!_isAwake)
            SwitchState(wakeState);
    }

    private void TriggerRealization()
    {
        if(_isAwake)
            SwitchState(realizationPlayerState);
    }

    private float RandomizeTimeToRoam()
    {
        return UnityEngine.Random.Range(m_MinTimeToRoamToNextLocation, m_MaxTimeToRoamToNextLocation);
    }

    public bool CheckStateWithState(EnemyBaseState state)
    {
        if(_currentState == state)
        { 
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Found something");

        _currentState.OnTriggerEnterState(other, this);

        if(_isAwake)
        {
            if (other.gameObject.tag.ToLower() == "player")
            {
                Debug.Log("Found player");

                other.gameObject.GetComponentInParent<PlayerDeathManager>().KillPlayer();
            }
        }
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);

        onStateChanged.Invoke();
    }
}
