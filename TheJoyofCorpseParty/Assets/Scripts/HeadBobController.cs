using System.Runtime.CompilerServices;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    private float _amplitude = 0.015f;
    private float _frequency = 10.0f;

    [SerializeField, Range(0, 0.1f)] private float _walkAmplitude = 0.015f;
    [SerializeField, Range(0, 0.1f)] private float _runningAmplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _walkFrequency = 10.0f;
    [SerializeField, Range(0, 30)] private float _runningFrequency = 20.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private Rigidbody _rb;
    private PlayerMovement movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startPos = _camera.localPosition;
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enable) return;

        MovementState currentState = movement.currentState;

        if(currentState == MovementState.sprinting)
        {
            _amplitude = _runningAmplitude;
            _frequency = _runningFrequency;
        }
        else if(currentState == MovementState.walking)
        {
            _amplitude = _walkAmplitude;
            _frequency = _walkFrequency;
        }


        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private void CheckMotion()
    {
        float speed = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z).magnitude;

        if (speed < _toggleSpeed) return;

        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }

    private void ResetPosition()
    {
        if(_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
