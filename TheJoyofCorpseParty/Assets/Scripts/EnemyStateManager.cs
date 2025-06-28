using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent mAgent;

    public EnemyIdleState idleState;
    public EnemyRoamState roamState;

    EnemyBaseState _currentState;

    [Header("Speed")]
    public float m_WalkSpeed;
    public float m_RunSpeed;

    [Header("Acceleration")]
    public float m_WalkAcceleration;
    public float m_RunAcceleration;

    [Header("Time To Roam (s)")]
    [Tooltip("Write it in seconds")]
    [SerializeField]
    private float m_MinTimeToRoamToNextLocation;

    [Tooltip("Write it in seconds")]
    [SerializeField]
    private float m_MaxTimeToRoamToNextLocation;

    public float m_TimeToRoam { get { return RandomizeTimeToRoam(); } }

    [Header("List destinations")]
    [SerializeField]
    private Transform[] m_Destinations;

    public Transform[] Destinations {  get { return m_Destinations; } }

    void Awake()
    {
        mAgent = GetComponent<NavMeshAgent>();

        idleState = new EnemyIdleState();
        roamState = new EnemyRoamState();
    }

    private void Start()
    {
        _currentState = idleState;
        _currentState.EnterState(this);

        if(m_MaxTimeToRoamToNextLocation < m_MinTimeToRoamToNextLocation)
        {
            Debug.LogError("Max time to roam is lower than min which can cause problems");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    private float RandomizeTimeToRoam()
    {
        return Random.Range(m_MinTimeToRoamToNextLocation, m_MaxTimeToRoamToNextLocation);
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnterState(other, this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }
}
