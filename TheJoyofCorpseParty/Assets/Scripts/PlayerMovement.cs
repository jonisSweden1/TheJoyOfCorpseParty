using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    float m_MoveSpeed;

    [SerializeField]
    private float walkSpeed, sprintSpeed;

    [SerializeField]
    private float m_GroundDrag;

    [Header("Crounching")]

    [SerializeField]
    private float m_CrouchSpeed;

    [SerializeField]
    private float crouchYScale;
    float startYScale;
    
    [Header("Keybinds")]
    [SerializeField]
    private InputActionReference m_MoveInputReference;

    [SerializeField]
    private InputActionReference m_SprintKey;

    [SerializeField]
    private InputActionReference m_CrouchKey;

    [SerializeField]
    private Transform m_Orientation;

    float horizontalInput, verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    PlayerGroundCheck groundCheck;

    private MovementState state;

    public enum MovementState
    {
        walking, sprinting, crouching
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        groundCheck = GetComponent<PlayerGroundCheck>();

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        GetInput();
        SpeedControl();
        StateHandler();

        // Handle drag
        if (groundCheck.Grounded)
            rb.linearDamping = m_GroundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        Vector2 input = m_MoveInputReference.action.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;

        if(m_CrouchKey.action.IsPressed())
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if(m_CrouchKey.action.IsPressed())
        {
            state = MovementState.crouching;
            m_MoveSpeed = m_CrouchSpeed;
        }

        // Mode - Sprinting
        if(groundCheck.Grounded && m_SprintKey.action.IsPressed())
        {
            state = MovementState.sprinting;
            m_MoveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (groundCheck.Grounded)
        {
            state = MovementState.walking;
            m_MoveSpeed = walkSpeed;
        }
    }

    private void MovePlayer()
    {
        moveDirection = m_Orientation.forward * verticalInput + m_Orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * m_MoveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if(flatVel.magnitude > m_MoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * m_MoveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
