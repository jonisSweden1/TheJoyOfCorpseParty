using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSettingModel[] audioSettings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SaveVolumes()
    {
        SaveLoadManager.Instance.Save(audioSettings, Application.persistentDataPath + "/audioSettings.json");
    }

    public AudioSettingModel[] LoadVolumes()
    {
        AudioSettingModel[] loadedSettings = SaveLoadManager.Instance.Load<AudioSettingModel[]>(Application.persistentDataPath + "/audioSettings.json");
        if (loadedSettings != null)
        {
            audioSettings = loadedSettings;
        }
        return audioSettings;
    }
}
