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
    }
}
