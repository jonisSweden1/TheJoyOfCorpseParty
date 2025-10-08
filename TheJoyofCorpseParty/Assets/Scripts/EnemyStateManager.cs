using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AgentMovement))]
[RequireComponent(typeof(EnemyDetectionSystem))]
public class EnemyStateManager : MonoBehaviour
{
    public EnemyDetectionSystem m_Enemy_Detection_System {  get; private set; }
    public EnemyNavigationSystem m_Enemy_Navigation_System { get; private set; }

    public EnemyIdleState idleState;
    public EnemyRoamState roamState;
    public EnemySeekState seekState;
    public EnemyChaseState chaseState;

    EnemyBaseState _currentState;

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

    [Header("List destinations")]
    [SerializeField]
    private Transform[] m_Destinations;

    [SerializeField]
    private Transform m_RootListDestinations;

    public Transform[] Destinations { get { return m_Destinations; } }

    public event Action onStateChanged;

    void Awake()
    {
        m_Enemy_Detection_System = GetComponent<EnemyDetectionSystem>();
        m_Enemy_Navigation_System = GetComponent<EnemyNavigationSystem>();

        idleState = new EnemyIdleState();
        roamState = new EnemyRoamState();
        seekState = new EnemySeekState();
        chaseState = new EnemyChaseState();

        if (m_RootListDestinations != null)
        {
            m_Destinations = GetAllDestinations();
        }
    }

    private Transform[] GetAllDestinations()
    {
        List<Transform> destinations = new List<Transform>();

        foreach(Transform t in m_RootListDestinations)
        {
            destinations.Add(t);
        }

        return destinations.ToArray();
    }

    private void Start()
    {
        _currentState = idleState;
        _currentState.EnterState(this);

        if(m_MaxTimeToRoamToNextLocation < m_MinTimeToRoamToNextLocation)
        {
            Debug.LogError("Maximum time to roam is lower than minimum. It can break the randomizer");
        }
    }

    void OnEnable()
    {
        m_Enemy_Detection_System.m_OnDetected += TriggerSeek;
    }

    void OnDisable()
    {
        m_Enemy_Detection_System.m_OnDetected -= TriggerSeek;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    private void TriggerSeek()
    {
        SwitchState(seekState);
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

        if (other.gameObject.tag.ToLower() == "player")
        {
            Debug.Log("Found player");

            other.gameObject.GetComponentInParent<PlayerDeathManager>().KillPlayer();
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
