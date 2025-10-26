using System.Collections.Generic;
using UnityEngine;

public enum EHeardSoundCategory
{
    EFootstep,
    ESoft,
    ELoud
}

public class HearingManager : MonoBehaviour
{
    public static HearingManager instance;

    public List<EnemySoundDetection> AllSensors {  get; private set; } = new List<EnemySoundDetection>();

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Register(EnemySoundDetection sensor)
    {
        AllSensors.Add(sensor);
    }

    public void Unregister(EnemySoundDetection sensor)
    {
        AllSensors.Remove(sensor);
    }

    public void OnSoundEmitted(Vector3 location, EHeardSoundCategory category, float intensity)
    {
        //Notify all the sensors
        foreach(var sensor in AllSensors)
        {
            sensor.OnHeardSound(location, category, intensity);
        }
    }
}
