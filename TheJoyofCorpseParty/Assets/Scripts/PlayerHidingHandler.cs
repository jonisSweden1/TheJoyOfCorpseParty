using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHidingHandler : MonoBehaviour
{
    [SerializeField]
    LayerMask hidingMask;

    [SerializeField]
    private float distance;

    public static event Action<Transform> m_OnUse;

    [Header("Keybinds")]
    [SerializeField] private InputActionReference m_UseKey;

    private bool isHidingSpotFound;
    private Transform cameraPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_UseKey.action.performed += UseAction_performed;
    }

    private void UseAction_performed(InputAction.CallbackContext obj)
    {
        if(isHidingSpotFound)
        {
            m_OnUse.Invoke(cameraPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, hidingMask))
        {
            if (hit.collider.tag == "HideSpot")
            {
                cameraPos = hit.collider.transform.GetChild(0);
                isHidingSpotFound = true;
            }
        }
        else
        {
            isHidingSpotFound = false;
        }
    }
}
