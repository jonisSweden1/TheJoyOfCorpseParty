using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("UI Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider cutscenesSlider;
    [SerializeField] private Slider ambienceSlider;

    [Header("Apply Button")]
    [SerializeField] private Button applyButton;

    // Temporary variables to hold the volume values where if the user cancels the changes, we can revert back to these values
    float tempMasterVolumeSlider;
    float tempMusicVolumeSlider;
    float tempSFXVolumeSlider;
    float tempCutscenesVolumeSlider;
    float tempAmbienceVolumeSlider;

    private bool isChanged = false;

    private void OnEnable()
    {
        tempMasterVolumeSlider = masterSlider.value;
        tempMusicVolumeSlider = musicSlider.value;
        tempAmbienceVolumeSlider = ambienceSlider.value;
        tempSFXVolumeSlider = sfxSlider.value;
        tempCutscenesVolumeSlider = cutscenesSlider.value;

        applyButton.onClick.AddListener(() => ApplyChanges());

        masterSlider.onValueChanged.AddListener(UpdateMasterTemp);
        musicSlider.onValueChanged.AddListener(UpdateMusicTemp);
        sfxSlider.onValueChanged.AddListener(UpdateSFXTemp);
        cutscenesSlider.onValueChanged.AddListener(UpdateCutscenesTemp);
        ambienceSlider.onValueChanged.AddListener(UpdateAmbienceTemp);

        CheckAndCompareCurrentAndChangedVolume();
    }

    private void OnDisable()
    {
        if (isChanged)
        {
            UIManager.instance.ShowPopUp("Changes has already been cancelled");
            CancelChanges();
        }

        applyButton.onClick.RemoveAllListeners();

        masterSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
        cutscenesSlider.onValueChanged.RemoveAllListeners();
        ambienceSlider.onValueChanged.RemoveAllListeners();
    }

    public void UpdateMasterTemp(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        UpdateSlidersFromAudioMixer();
        CheckAndCompareCurrentAndChangedVolume();
    }

    public void UpdateMusicTemp(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        UpdateSlidersFromAudioMixer();
        CheckAndCompareCurrentAndChangedVolume();
    }
    public void UpdateSFXTemp(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        UpdateSlidersFromAudioMixer();
        CheckAndCompareCurrentAndChangedVolume();
    }
    public void UpdateCutscenesTemp(float value)
    {
        audioMixer.SetFloat("CutsceneVolume", Mathf.Log10(value) * 20);
        UpdateSlidersFromAudioMixer();
        CheckAndCompareCurrentAndChangedVolume();
    }
    public void UpdateAmbienceTemp(float value)
    {
        audioMixer.SetFloat("AmbienceVolume", Mathf.Log10(value) * 20);
        UpdateSlidersFromAudioMixer();
        CheckAndCompareCurrentAndChangedVolume();
    }

    private void CheckAndCompareCurrentAndChangedVolume()
    {
        if(masterSlider.value != tempMasterVolumeSlider || 
            musicSlider.value != tempMusicVolumeSlider || 
            sfxSlider.value != tempSFXVolumeSlider || 
            cutscenesSlider.value != tempCutscenesVolumeSlider || 
            ambienceSlider.value != tempAmbienceVolumeSlider)
        {
            applyButton.interactable = true;
            isChanged = true;
        }
        else
        {
            applyButton.interactable = false;
            isChanged = false;
        }
    }

    public void ApplyChanges()
    {
        AudioManager.Instance.SaveVolumes();

        tempMasterVolumeSlider = masterSlider.value;
        tempMusicVolumeSlider = musicSlider.value;
        tempAmbienceVolumeSlider = ambienceSlider.value;
        tempSFXVolumeSlider = sfxSlider.value;
        tempCutscenesVolumeSlider = cutscenesSlider.value;

        applyButton.interactable = false;
        isChanged = false;
    }

    public void CancelChanges()
    {
        masterSlider.value = tempMasterVolumeSlider;
        musicSlider.value = tempMusicVolumeSlider;
        sfxSlider.value = tempSFXVolumeSlider;
        cutscenesSlider.value = tempCutscenesVolumeSlider;
        ambienceSlider.value = tempAmbienceVolumeSlider;
        UpdateSlidersFromAudioMixer();
    }

    private void UpdateSlidersFromAudioMixer()
    {
        // Check if the audioMixer is assigned before trying to get the volume values
        if (audioMixer == null) {
            Debug.LogError("AudioMixer is not assigned in the AudioSettings.");
            return;
        }

        // Check if volumes can be retrieved from the AudioMixer, and log an error if not
        if (!audioMixer.GetFloat("MasterVolume", out float masterVolume)) {
            Debug.LogError("Failed to get MasterVolume from AudioMixer.");
            return;
        }

        if(!audioMixer.GetFloat("MusicVolume", out float musicVolume)) {
            Debug.LogError("Failed to get MusicVolume from AudioMixer.");
            return;
        }

        if(!audioMixer.GetFloat("SFXVolume", out float sfxVolume)) {
            Debug.LogError("Failed to get SFXVolume from AudioMixer.");
            return;
        }

        if(!audioMixer.GetFloat("CutsceneVolume", out float cutscenesVolume)) {
            Debug.LogError("Failed to get CutsceneVolume from AudioMixer.");
            return;
        }

        if(!audioMixer.GetFloat("AmbienceVolume", out float ambienceVolume)) {
            Debug.LogError("Failed to get AmbienceVolume from AudioMixer.");
            return;
        }

        // Assign the retrieved volume values to the sliders, converting from decibels to slider values
        masterSlider.value = Mathf.Pow(10, masterVolume / 20);
        musicSlider.value = Mathf.Pow(10, musicVolume / 20);
        sfxSlider.value = Mathf.Pow(10, sfxVolume / 20);
        cutscenesSlider.value = Mathf.Pow(10, cutscenesVolume / 20);
        ambienceSlider.value = Mathf.Pow(10, ambienceVolume / 20);
    }
}
