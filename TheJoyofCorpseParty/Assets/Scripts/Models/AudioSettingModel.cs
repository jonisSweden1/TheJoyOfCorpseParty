using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[Serializable]
public class AudioSettingModel
{
    public float volume;
    public Slider volumeSlider;
    public string volumeName;
}

public class AudioSettingDTO
{
    public float volume;
    public string volumeName;
}
