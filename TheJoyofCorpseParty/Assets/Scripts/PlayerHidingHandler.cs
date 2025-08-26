using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHidingHandler : MonoBehaviour
{
    [SerializeField]
    LayerMask hidingMask;

    [SerializeField]
    private float distance;

    private bool isHidingSpotFound;
    private Transform cameraTrans;

    public bool FindHidingSpot(out Vector3 cameraPos)
    {
        if(isHidingSpotFound)
        {
            cameraPos = cameraTrans.position;
            return true;
        }
        else
        {
            cameraPos = Vector3.zero;
            return false;
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
                cameraTrans = hit.collider.transform.GetChild(0);
                isHidingSpotFound = true;
            }
        }
        else
        {
            isHidingSpotFound = false;
        }
    }
}
