using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[Serializable]
public class AudioSettingModel
{
    public AudioMixerGroup mixerGroup;
    public Slider volumeSlider;
    public string volumeName;
}
