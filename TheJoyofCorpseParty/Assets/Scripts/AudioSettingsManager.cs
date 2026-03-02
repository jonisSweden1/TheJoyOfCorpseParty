using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour, ICheckChangeDetection
{
    [SerializeField]
    private AudioSettingModel[] audioSettings;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Button applyButton;

    private bool hasUnsavedChanges = false;

    public void Start()
    {
        if(audioSettings == null || audioSettings.Length == 0)
        {
            Debug.LogWarning("AudioSettingsManager: No audio settings configured.");
            return;
        }

        if(applyButton != null)
        {
            applyButton.interactable = false;
            CalculateDifferencesWithSaveVolumes();
        }

        UpdateSliders();
    }

    public void SetVolume(Slider slider)
    {
        if(audioSettings == null || audioSettings.Length == 0)
        {
            Debug.LogWarning("AudioSettingsManager: No audio settings configured.");
            return;
        }

        foreach (AudioSettingModel setting in audioSettings)
        {
            if (setting.volumeSlider == slider)
            {
                float volume = slider.value;
                audioMixer.SetFloat(setting.volumeName, Mathf.Log10(volume) * 20);
                break;
            }
        }

        UpdateSliders();

        if(applyButton != null)
            CalculateDifferencesWithSaveVolumes();
    }

    private void UpdateSliders()
    {
        foreach (AudioSettingModel setting in audioSettings)
        {
            float volume;
            if (audioMixer.GetFloat(setting.volumeName, out volume))
            {
                Debug.Log("Got float from mixer: " + volume);
                setting.volumeSlider.value = Mathf.Pow(10, volume / 20);
            }
        }
    }

    // Calculate the differences between the current slider values and the saved volumes in the AudioMixer. If there are any differences, enable the apply button and set hasUnsavedChanges to true.
    private void CalculateDifferencesWithSaveVolumes()
    {
        foreach (AudioSettingModel setting in audioSettings)
        {
            float currentVolume = setting.volumeSlider.value;
            float savedVolume = audioMixer.GetFloat(setting.volumeName, out savedVolume) ? Mathf.Pow(10, savedVolume / 20) : 0f;

            if (currentVolume - savedVolume != 0.00f)
            {
                Debug.Log($"There is a difference in volume, from where the saved volume ({savedVolume}) and the current volume ({currentVolume}) has difference in {currentVolume - savedVolume}");

                applyButton.interactable = true;
                hasUnsavedChanges = true;
                return;
            }
        }
    }

    public void SaveVolumes()
    {
        AudioManager.Instance.SaveVolumes();

        foreach (AudioSettingModel setting in audioSettings)
        {
            float volume;
            if (audioMixer.GetFloat(setting.volumeName, out volume))
            {
                
            }
        }
        applyButton.interactable = false;
        hasUnsavedChanges = false;
    }

    public bool HasUnsavedChanges()
    {
        return hasUnsavedChanges;
    }
}


