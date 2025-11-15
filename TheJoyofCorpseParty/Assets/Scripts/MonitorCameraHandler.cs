using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonitorCameraHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference OpenCloseTabletAction;
    [SerializeField] private InputActionReference NextCamera;
    [SerializeField] private InputActionReference PrevCamera;

    private void OnEnable()
    {
        OpenCloseTabletAction.action.performed += OpenCloseTablet;
        NextCamera.action.performed += SwitchToNextCamera;
        PrevCamera.action.performed += SwitchToPreviousCamera;
    }

    private void OnDisable()
    {
        OpenCloseTabletAction.action.performed -= OpenCloseTablet;
        NextCamera.action.performed -= SwitchToNextCamera;
        PrevCamera.action.performed -= SwitchToPreviousCamera;
    }

    private void SwitchToPreviousCamera(InputAction.CallbackContext context)
    {
        if(CameraManager.instance.CheckOpenCam)
        {
            CameraManager.instance.ChangeToPreviousCamera();
        }
    }

    private void SwitchToNextCamera(InputAction.CallbackContext context)
    {
        if (CameraManager.instance.CheckOpenCam)
        {
            CameraManager.instance.ChangeToNextCamera();
        }
    }

    private void OpenCloseTablet(InputAction.CallbackContext obj)
    {
        if(!CameraManager.instance.CheckOpenCam)
        {
            CameraManager.instance.OpenMonitorCam();
        }
        else
        {
            CameraManager.instance.CloseMonitorCam();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
