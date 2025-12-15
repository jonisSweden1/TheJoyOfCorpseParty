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

    public static CameraManager instance;

    private int monitorCamCountIndex = 0;

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
            currentMonitorCam = monitorCameras[monitorCamCountIndex];
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
            mainCamera.GetComponent<AudioListener>().enabled = false;
            EnableMonitorCamera(currentMonitorCam);
            onOpenCam = true;
        }
    }

    public void CloseMonitorCam()
    {
        if (mainCamera != null && monitorCameras != null)
        {
            mainCamera.enabled = true;
            mainCamera.GetComponent<AudioListener>().enabled = true;
            DisableMonitorCamera(currentMonitorCam);
            onOpenCam = false;
        }
    }

    public void ChangeToNextCamera()
    {
        monitorCamCountIndex++;

        if(monitorCamCountIndex > monitorCameras.Length - 1)
        {
            monitorCamCountIndex = 0;
        }

        currentMonitorCam = monitorCameras[monitorCamCountIndex];

        EnableMonitorCamera(monitorCameras[monitorCamCountIndex]);

        StartCoroutine(CheckAllCamera());
    }

    public void ChangeToPreviousCamera()
    {
        monitorCamCountIndex--;

        if(monitorCamCountIndex < 0)
        {
            monitorCamCountIndex = monitorCameras.Length - 1;
        }

        currentMonitorCam = monitorCameras[monitorCamCountIndex];

        EnableMonitorCamera(monitorCameras[monitorCamCountIndex]);

        StartCoroutine(CheckAllCamera());
    }

    void EnableMonitorCamera(CamInfo cam)
    {
        cam.cam.enabled = true;
        cam.cam.GetComponent<AudioListener>().enabled = true;
        cam.light.enabled = true;
    }

    void DisableMonitorCamera(CamInfo cam)
    {
        cam.cam.enabled = false;
        cam.cam.GetComponent<AudioListener>().enabled = false;
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
