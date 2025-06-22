using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class YBotAnimationStateController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private InputActionReference actionReference;

    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;

    int velocityHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();

        velocityHash = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        Vector2 moveInput = actionReference.action.ReadValue<Vector2>();

        if(moveInput.y > 0.01)
        {
            if(velocity < 1)
            Accelerate();
        }
        else if(velocity > 0.00f)
        {
            Decelerate();
        }
    }

    private void Decelerate()
    {
        velocity -= Time.deltaTime * deceleration;

        _animator.SetFloat(velocityHash, velocity);
    }

    private void Accelerate()
    {
        velocity += Time.deltaTime * acceleration;

        _animator.SetFloat(velocityHash, velocity);
    }
}
