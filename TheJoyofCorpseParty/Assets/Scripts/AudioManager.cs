using System.Text;
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
        if (SaveLoadManager.Instance.CheckIfFileExists("audioSettings"))
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
        // Convert string into a StringBuilder for better performance when concatenating strings and to build the JSON string for saving the audio settings to a file.
        StringBuilder sb = new StringBuilder();

        sb.Append("{\n");

        foreach (AudioSettingModel setting in audioSettings)
        {
            AudioSettingDTO settingDTO = new AudioSettingDTO
            {
                volumeName = setting.volumeName,
                volume = setting.volume
            };

            float volume;
            if (audioMixer.GetFloat(settingDTO.volumeName, out volume))
            {
                settingDTO.volume = volume;
                sb.AppendFormat("  \"{0}\": {1},\n", settingDTO.volumeName, settingDTO.volume);
            }
        }

        sb.Append("}");

        SaveLoadManager.Instance.Save(sb.ToString(), "audioSettings");

        Debug.Log("Save volumes");
    }

    public AudioSettingModel[] LoadVolumes()
    {
        string loadJsonString = SaveLoadManager.Instance.Load("audioSettings");

        AudioSettingDTO[] loadedSettingsDTO = JsonUtility.FromJson<AudioSettingDTO[]>(loadJsonString);

        AudioSettingModel[] loadedSettings = new AudioSettingModel[loadedSettingsDTO.Length];
        
        foreach (var settingDTO in loadedSettingsDTO)
        {
            AudioSettingModel settingModel = new AudioSettingModel
            {
                volumeName = settingDTO.volumeName,
                volume = settingDTO.volume
            };
            loadedSettings[System.Array.IndexOf(loadedSettingsDTO, settingDTO)] = settingModel;
        }

        return loadedSettings;
    }
}
