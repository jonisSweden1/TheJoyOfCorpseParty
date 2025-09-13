using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlashlightHandler : MonoBehaviour
{
    public event Action onFlashLight;

    [SerializeField]
    private InputActionReference _flashlightInput;

    private Light _light;
    private bool _isTurnOn;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isTurnOn = false;

        if(_isTurnOn)
        {
            _light.enabled = true;
            _isTurnOn = true;
        }
        else
        {
            _light.enabled = false;
            _isTurnOn = false;
        }
    }

    private void OnEnable()
    {
        if (_flashlightInput != null)
        {
            _flashlightInput.action.performed += Flashlight_Action_performed;
        }
    }

    private void OnDisable()
    {
        if(_flashlightInput != null)
        {
            _flashlightInput.action.performed -= Flashlight_Action_performed;
        }
    }

    private void Flashlight_Action_performed(InputAction.CallbackContext obj)
    {
        onFlashLight.Invoke();

        if(_isTurnOn)
        {
            _light.enabled = false;
            _isTurnOn = false;
        }
        else
        {
            _light.enabled = true;
            _isTurnOn = true;
        }
    }
}
