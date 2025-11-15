using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSecurityRoomController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference navigationController;

    [SerializeField] private InputActionReference openCloseCamera;

    [SerializeField]
    private float speedInNavigation = 0.0f;

    private float rotationY = 0.0f;

    private PlayerControllerState _currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentState = PlayerControllerState.Forward;
    }

    private void OnEnable()
    {
        navigationController.action.performed += ActionLookBehindPerformed;
    }

    private void OnDisable()
    {
        navigationController.action.performed -= ActionLookBehindPerformed;
    }

    private void ActionLookBehindPerformed(InputAction.CallbackContext obj)
    {
        if(_currentState == PlayerControllerState.Forward)
        {
            _currentState = PlayerControllerState.Backward;
        }
        else
        {
            _currentState = PlayerControllerState.Forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState == PlayerControllerState.Backward)
        {
            if(rotationY < 180)
            {
                rotationY += speedInNavigation * Time.deltaTime;

                Quaternion rotation = Quaternion.Euler(0, rotationY, 0);

                transform.localRotation = rotation;
            }
        }
        else
        {
            if (rotationY > 0)
            {
                rotationY -= speedInNavigation * Time.deltaTime;

                Quaternion rotation = Quaternion.Euler(0, rotationY, 0);

                transform.localRotation = rotation;
            }
        }
    }
}

public enum PlayerControllerState
{
    Forward, Backward
}
