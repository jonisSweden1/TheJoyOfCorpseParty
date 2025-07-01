using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    private InputActionReference m_MoveInputReference;

    [Header("Movement")]
    float m_MoveSpeed;

    [SerializeField]
    private float walkSpeed, sprintSpeed;

    [SerializeField]
    private float m_GroundDrag;

    [SerializeField]
    private Transform m_Orientation;

    float horizontalInput, verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    PlayerGroundCheck groundCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        groundCheck = GetComponent<PlayerGroundCheck>();
    }

    private void Update()
    {
        GetInput();
        SpeedControl();

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
