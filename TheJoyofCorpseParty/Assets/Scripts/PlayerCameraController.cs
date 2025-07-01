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

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
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
