using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    InputActionReference _lookInputReference;

    [SerializeField]
    private float sensX, sensY;

    [SerializeField]
    private Transform _orientation;

    float xRotation;
    float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        PlayerDeathManager.m_OnDeath += DisableCamera;
    }

    private void OnDisable()
    {
        PlayerDeathManager.m_OnDeath -= DisableCamera;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void DisableCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.enabled = false;
    }

    void RotateCamera()
    {
        Vector2 mouse = _lookInputReference.action.ReadValue<Vector2>();
        float mouseX = mouse.x * Time.deltaTime * sensX;
        float mouseY = mouse.y * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        
        _orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
