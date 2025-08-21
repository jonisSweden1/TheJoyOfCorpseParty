using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    float m_MoveSpeed;

    [SerializeField] private float walkSpeed, sprintSpeed;
    [SerializeField] private float m_GroundDrag;

    [Header("Crounching")]

    [SerializeField] private float m_CrouchSpeed;

    [SerializeField] private float crouchYScale;
    float startYScale;

    [Header("Step Climb")]
    [SerializeField] Transform stepRayUpper;
    [SerializeField] Transform stepRayLower;

    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 0.1f;

    [Header("Keybinds")]
    [SerializeField] private InputActionReference m_MoveInputReference;

    [SerializeField] private InputActionReference m_SprintKey;

    [SerializeField] private InputActionReference m_CrouchKey;

    [SerializeField] private Transform m_Orientation;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        groundCheck = GetComponent<PlayerGroundCheck>();
        playerSlopeHandler = GetComponent<PlayerSlopeHandler>();

        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }

    private void Start()
    {
        startYScale = transform.localScale.y;
    }

    private void OnEnable()
    {
        m_CrouchKey.action.performed += Crouch_performed;
        m_CrouchKey.action.canceled += Crouch_canceled;
    }

    private void OnDisable()
    {
        m_CrouchKey.action.performed -= Crouch_performed;
        m_CrouchKey.action.canceled -= Crouch_canceled;
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
        StepClimb();
    }

    private void GetInputCrouch()
    {
        if(m_CrouchKey != null)
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

            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        rb.AddForce(moveDirection.normalized * m_MoveSpeed * 10f, ForceMode.Force);

        rb.useGravity = !playerSlopeHandler.OnSlope();
    }

    private void SpeedControl()
    {
        if(playerSlopeHandler.OnSlope())
        {
            if (rb.linearVelocity.magnitude > m_MoveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * m_MoveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            if (flatVel.magnitude > m_MoveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * m_MoveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, playerSlopeHandler.SlopeHit.normal).normalized;
    }

    private void StepClimb()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (state == MovementState.walking)
        {
            RaycastHit hitLower;
            if (Physics.Raycast(stepRayLower.position, m_Orientation.TransformDirection(Vector3.forward), out hitLower, 0.4f))
            {
                //Debug.Log("Hitlower: " + hitLower.transform.name);

                RaycastHit hitUpper;
                if (Physics.Raycast(stepRayUpper.position, m_Orientation.TransformDirection(Vector3.forward), out hitUpper, 0.6f))
                {
                    //Debug.Log("Hitupper: " + hitUpper.transform.name);

                    if (hitUpper.transform.tag == "Stairs")
                    {
                        //Debug.Log("Moving up");
                        rb.position -= new Vector3(0f, -stepSmooth, 0f);
                    }
                }
            }

            RaycastHit hitLower45;
            if (Physics.Raycast(stepRayLower.position, m_Orientation.TransformDirection(1.5f, 0, 1), out hitLower45, 0.4f))
            {
                RaycastHit hitUpper45;
                if (Physics.Raycast(stepRayUpper.position, m_Orientation.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.6f))
                {
                    if (hitUpper45.transform.tag == "Stairs")
                    {
                        Debug.Log("Moving up");
                        rb.position -= new Vector3(0f, -stepSmooth, 0f);
                    }
                }
            }

            RaycastHit hitLowerMinus45;
            if (Physics.Raycast(stepRayLower.position, m_Orientation.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.4f))
            {
                RaycastHit hitUpperMinus45;
                if (Physics.Raycast(stepRayUpper.position, m_Orientation.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.6f))
                {
                    if (hitUpperMinus45.transform.tag == "Stairs")
                    {
                        Debug.Log("Moving up");
                        rb.position -= new Vector3(0f, -stepSmooth, 0f);
                    }
                }
            }
        }
    }
}
