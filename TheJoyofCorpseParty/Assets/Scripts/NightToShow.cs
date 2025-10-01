using System;
using TMPro;
using UnityEngine;

public class NightToShow : MonoBehaviour
{
    /*
    [SerializeField]
    private NightToShowData[] dataToShow;
    */

    public NightToShowData nightOneData;
    public NightToShowData nightTwoData;

    [Header("Components To Set")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;

    private NightDataEnum _dataEnum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string rawData = DataTransferToScene.message;

        Debug.Log(rawData);

        ConvertDataToNightEnum(rawData, out _dataEnum);

        IdentifyNight(_dataEnum);
    }

    void ConvertDataToNightEnum(string data, out NightDataEnum enumerator)
    {
        enumerator = NightDataEnum.None;

        if (!string.IsNullOrEmpty(data))
        {
            object objectEnum;

            if(Enum.TryParse(typeof(NightDataEnum), data, true, out objectEnum))
            {
                enumerator = (NightDataEnum)objectEnum;
            }
        }
    }

    void IdentifyNight(NightDataEnum night)
    {
        switch (night)
        {
            case NightDataEnum.Night1:
                WriteTitle(nightOneData.title);
                WriteDescription(nightOneData.description);
                break;
            case NightDataEnum.Night2:
                WriteTitle(nightTwoData.title);
                WriteDescription(nightTwoData.description);
                break;
        }
    }

    void WriteTitle(string title)
    {
        _title.text = title;
    }

    void WriteDescription(string description)
    {
        _description.text = description;
    }

    public void LoadSceneByNightId()
    {
        switch (_dataEnum)
        {
            case NightDataEnum.Night1:
                LevelManager.Instance.LoadSceneAsync(nightOneData.sceneBuildId);
                break;
            case NightDataEnum.Night2:
                LevelManager.Instance.LoadSceneAsync(nightTwoData.sceneBuildId);
                break;
            case NightDataEnum.None:
                Debug.LogError("There is no scene to load");
                break;
        }
    }
}

public enum NightDataEnum
{
    None, Night1, Night2, Night3, Night4, Night5
}
