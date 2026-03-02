using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSettingModel[] audioSettings;
    public AudioMixer audioMixer;

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

    private void Start()
    {
        if (SaveLoadManager.Instance.CheckIfFileExists<AudioSettingModel>(Application.persistentDataPath + "/audioSettings.json"))
        {
            audioSettings = LoadVolumes();

            foreach (var volume in audioSettings)
            {
                audioMixer.SetFloat(volume.volumeName, volume.volume);
            }
        }
    }

    public void SaveVolumes()
    {
        SaveLoadManager.Instance.Save(audioSettings, Application.persistentDataPath + "/audioSettings.json");

        Debug.Log("Save volumes");
    }

    public AudioSettingModel[] LoadVolumes()
    {
        AudioSettingModel[] loadedSettings = SaveLoadManager.Instance.Load<AudioSettingModel[]>(Application.persistentDataPath + "/audioSettings.json");
        return loadedSettings;
    }
}
