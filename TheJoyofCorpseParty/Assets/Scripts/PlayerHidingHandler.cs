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

    public bool FindHidingSpot(out Transform cameraPos)
    {
        if(isHidingSpotFound)
        {
            Debug.Log("Hiding spot is found");
            cameraPos = cameraTrans;
            return true;
        }
        else
        {
            Debug.Log("Hiding spot is not found");
            cameraPos = null;
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
                //Debug.Log("Hiding spot Found!");
                cameraTrans = hit.collider.transform.GetChild(0);
                //Debug.Log(cameraTrans);
                
                isHidingSpotFound = true;
            }
        }
        else
        {
            isHidingSpotFound = false;
        }
    }
}
