using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("UI Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider cutscenesSlider;
    [SerializeField] private Slider ambienceSlider;

    public static AudioManager Instance { get; private set; }

    float masterVolume;
    float musicVolume;
    float sfxVolume;
    float cutscenesVolume;
    float ambienceVolume;

    private void Awake()
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
        LoadVolumes();
    }

    public void SaveVolumes()
    {
        // Transform the decibel values back to slider, linear values and save them to PlayerPrefs
        if (audioMixer != null)
        {
            audioMixer.GetFloat("MasterVolume", out masterVolume);
            audioMixer.GetFloat("MusicVolume", out musicVolume);
            audioMixer.GetFloat("SFXVolume", out sfxVolume);
            audioMixer.GetFloat("CutsceneVolume", out cutscenesVolume);
            audioMixer.GetFloat("AmbienceVolume", out ambienceVolume);

            PlayerPrefs.SetFloat("MasterVolume_Slider", Mathf.Pow(10, masterVolume / 20));
            PlayerPrefs.SetFloat("MusicVolume_Slider", Mathf.Pow(10, musicVolume / 20));
            PlayerPrefs.SetFloat("SFXVolume_Slider", Mathf.Pow(10, sfxVolume / 20));
            PlayerPrefs.SetFloat("CutsceneVolume_Slider", Mathf.Pow(10, cutscenesVolume / 20));
            PlayerPrefs.SetFloat("AmbienceVolume_Slider", Mathf.Pow(10, ambienceVolume / 20));
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("AudioMixer is not assigned in the AudioManager.");
        }
    }

    public void LoadVolumes()
    {
        if (audioMixer != null)
        {
            // Load the saved volume slider values from PlayerPrefs, and if the values don't exist, use the default value of 1f
            float tempMasterSliderVolume = PlayerPrefs.GetFloat("MasterVolume_Slider", 1f);
            float tempMusicSliderVolume = PlayerPrefs.GetFloat("MusicVolume_Slider", 1f);
            float tempSFXSliderVolume = PlayerPrefs.GetFloat("SFXVolume_Slider", 1f);
            float tempCutscenesSliderVolume = PlayerPrefs.GetFloat("CutsceneVolume_Slider", 1f);
            float tempAmbienceSliderVolume = PlayerPrefs.GetFloat("AmbienceVolume_Slider", 1f);

            // Set the slider values to the loaded values
            masterSlider.value = tempMasterSliderVolume;
            musicSlider.value = tempMusicSliderVolume;
            sfxSlider.value = tempSFXSliderVolume;
            cutscenesSlider.value = tempCutscenesSliderVolume;
            ambienceSlider.value = tempAmbienceSliderVolume;

            // Transform the slider values to decibels and set them in the audio mixer
            masterVolume = Mathf.Log10(tempMasterSliderVolume) * 20;
            musicVolume = Mathf.Log10(tempMusicSliderVolume) * 20;
            sfxVolume = Mathf.Log10(tempSFXSliderVolume) * 20;
            cutscenesVolume = Mathf.Log10(tempCutscenesSliderVolume) * 20;
            ambienceVolume = Mathf.Log10(tempAmbienceSliderVolume) * 20;

            audioMixer.SetFloat("MasterVolume", masterVolume);
            audioMixer.SetFloat("MusicVolume", musicVolume);
            audioMixer.SetFloat("SFXVolume", sfxVolume);
            audioMixer.SetFloat("CutsceneVolume", cutscenesVolume);
            audioMixer.SetFloat("AmbienceVolume", ambienceVolume);
        }
        else
        {
            Debug.LogError("AudioMixer is not assigned in the AudioManager.");
        }
    }
}
