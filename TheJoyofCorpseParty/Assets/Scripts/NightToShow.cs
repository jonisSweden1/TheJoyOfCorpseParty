using System;
using UnityEngine;

public class NightToShow : MonoBehaviour
{
    public NightToShowData nightOneData;
    public NightToShowData nightTwoData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(DataTransferToScene.messageData != null)
        {
            string data = DataTransferToScene.messageData;

            object parseData;

            if(Enum.TryParse(typeof(Night), data, out parseData))
            {
                Debug.Log(parseData);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[CreateAssetMenu(fileName = "NewNightToShowData", menuName = "Night To Show/Data")]
public class NightToShowData : ScriptableObject
{
    public string title;
    public string description;
}

public enum Night
{
    Night1 = 1, Night2, Night3, Night4, Night5
}
