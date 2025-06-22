using UnityEngine;
using UnityEngine.InputSystem;

public class YBotAnimationStateController : MonoBehaviour
{
    private Animator _animator;
    int isWalkingHash;

    [SerializeField]
    private InputActionReference actionReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");

        actionReference.action.performed += Action_performed;
        actionReference.action.canceled += Action_canceled;
    }

    private void Action_canceled(InputAction.CallbackContext obj)
    {
        SetWalking(false);
    }

    private void SetWalking(bool isWalking)
    {
        _animator.SetBool(isWalkingHash, isWalking);
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        SetWalking(true);
    }
}
