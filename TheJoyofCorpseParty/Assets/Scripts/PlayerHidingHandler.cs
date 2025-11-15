using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHidingHandler : MonoBehaviour
{
    [SerializeField]
    LayerMask hidingMask;

    [SerializeField]
    private float distance;

    [SerializeField]
    private Material selectionMaterial;

    private Material originalMaterial;

    private bool isHidingSpotFound;
    private CamHideInfoData _cameraInfo;
    private GameObject _hideSpot;

    private bool isHidingSpotTook = false;
    private bool isHidingSpotShown = false;

    public bool FindHidingSpot(out CamHideInfoData camInfoData)
    {
        if(isHidingSpotFound)
        {
            Debug.Log("Hiding spot is found");
            camInfoData = _cameraInfo;
            isHidingSpotTook = true;
            UnvisualizeTheSpot(_hideSpot);
            enabled = false;
            return true;
        }
        else
        {
            Debug.Log("Hiding spot is not found");
            camInfoData = null;
            return false;
        }
    }

    private void OnEnable()
    {
        isHidingSpotTook = false;
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
                CamHideInfo cameraInfoCam = hit.collider.transform.GetChild(0).gameObject.GetComponent<CamHideInfo>();
                _cameraInfo = cameraInfoCam.Data;
                _hideSpot = hit.collider.gameObject;

                if(!isHidingSpotTook && !isHidingSpotShown)
                {
                    VisualizeTheSpot(_hideSpot);
                    isHidingSpotShown =true;
                }
                //Debug.Log(cameraTrans);
                
                isHidingSpotFound = true;
            }
        }
        else
        {
            //Debug.Log("Unvisualize");

            UnvisualizeTheSpot(_hideSpot);
            isHidingSpotFound = false;
        }
    }

    void VisualizeTheSpot(GameObject hideSpot)
    {
        originalMaterial = hideSpot.GetComponent<MeshRenderer>().material;
        hideSpot.GetComponent<MeshRenderer>().material = selectionMaterial;
    }

    void UnvisualizeTheSpot(GameObject hideSpot)
    {
        if(originalMaterial != null)
        {
            hideSpot.GetComponent<MeshRenderer>().material = originalMaterial;
            isHidingSpotShown=false;
        }
    }
}
