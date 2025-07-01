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

    PlayerSlopeHandler playerSlopeHandler;

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
        playerSlopeHandler = GetComponent<PlayerSlopeHandler>();

        startYScale = transform.localScale.y;

        m_CrouchKey.action.performed += Crouch_performed;
        m_CrouchKey.action.canceled += Crouch_canceled;
    }

    private void Crouch_canceled(InputAction.CallbackContext obj)
    {
        GetInputCrouch();
    }

    private void Crouch_performed(InputAction.CallbackContext obj)
    {
        GetInputCrouch();
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

    private void GetInputCrouch()
    {
        if (m_CrouchKey.action.IsPressed())
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void GetInput()
    {
        Vector2 input = m_MoveInputReference.action.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
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
        else if(groundCheck.Grounded && m_SprintKey.action.IsPressed())
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

        if(playerSlopeHandler.OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * m_MoveSpeed * 20f, ForceMode.Force);
        }

        rb.AddForce(moveDirection.normalized * m_MoveSpeed * 10f, ForceMode.Force);

        rb.useGravity = !playerSlopeHandler.OnSlope();
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

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, playerSlopeHandler.SlopeHit.normal).normalized;
    }
}
