using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;
    private Animator m_Animator;
    private NavMeshAgent agent;

    private Vector2 velocity;
    private Vector2 smoothDeltaPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        m_Animator.applyRootMotion = true;
        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    private void OnAnimatorMove()
    {
        Vector3 rootPosition = m_Animator.rootPosition;
        rootPosition.y = agent.nextPosition.y;
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;
    }

    // Update is called once per frame
    void Update()
    {
        SynchronizeAnimatorAndAgent();
        HandleInput();
    }

    private void SynchronizeAnimatorAndAgent()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        velocity = smoothDeltaPosition / Time.deltaTime;
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            velocity = Vector2.Lerp(
                Vector2.zero, 
                velocity, 
                agent.remainingDistance / agent.stoppingDistance
             );
        }

        bool shouldMove = velocity.magnitude > 0.5f
            && agent.remainingDistance > agent.stoppingDistance;

        m_Animator.SetBool("move", shouldMove);
        m_Animator.SetFloat("Locomotion", velocity.magnitude);

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if(deltaMagnitude > agent.radius / 2f)
        {
            transform.position = Vector3.Lerp(
                m_Animator.rootPosition,
                agent.nextPosition,
                smooth
             );
        }
    }

    private void HandleInput()
    {
        if(Application.isFocused && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mask))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
