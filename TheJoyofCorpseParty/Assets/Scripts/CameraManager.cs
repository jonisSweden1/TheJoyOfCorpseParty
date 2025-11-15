using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Camera[] monitorCameras;

    private Camera currentCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(mainCamera != null)
        {
            currentCamera = mainCamera;
            StartCoroutine(CheckAllCamera());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckAllCamera()
    {
        foreach (Camera cam in monitorCameras)
        {
            yield return null;

            if (currentCamera != cam)
            {
                cam.enabled = false;
            }
        }
    }
}
