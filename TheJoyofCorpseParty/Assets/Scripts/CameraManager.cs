using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private CamInfo[] monitorCameras;

    private CamInfo currentMonitorCam;

    private bool onOpenCam = false;

    public CameraManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(monitorCameras != null)
        {
            currentMonitorCam = monitorCameras[0];
            StartCoroutine(CheckAllCamera());

            if(!onOpenCam)
            {
                DisableMonitorCamera(currentMonitorCam);
            }
        }
    }

    public bool CheckOpenCam {  get { return onOpenCam; } }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMonitorCam()
    {
        if(mainCamera != null && monitorCameras != null)
        {
            mainCamera.enabled = false;
            EnableMonitorCamera(currentMonitorCam);
        }
    }

    public void CloseMonitorCam()
    {

    }

    public void ChangeToNextCamera()
    {
        
    }

    public void ChangeToPreviousCamera()
    {

    }

    void EnableMonitorCamera(CamInfo cam)
    {
        cam.cam.enabled = true;
        cam.light.enabled = true;
    }

    void DisableMonitorCamera(CamInfo cam)
    {
        cam.cam.enabled = false;
        cam.light.enabled = false;
    }

    IEnumerator CheckAllCamera()
    {
        foreach (CamInfo camInfo in monitorCameras)
        {
            yield return null;

            if (currentMonitorCam != camInfo)
            {
                DisableMonitorCamera(camInfo);
            }
        }
    }
}
