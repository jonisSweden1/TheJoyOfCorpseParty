using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Text;
using System.IO;

// JSON Save/Load Manager
public class SaveLoadManager : MonoBehaviour
{
    private static string SAVE_FOLDER;

    public static SaveLoadManager Instance { get; private set; }

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
        SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
            Debug.Log("SaveLoadManager: Created save directory at " + SAVE_FOLDER);
        }
    }

    public void Save(string json, string name)
    {
        name = name.Replace(" ", "_");

        string destination = SAVE_FOLDER + name + ".json";
        File.WriteAllText(destination, json, Encoding.UTF8);
    }

    public string Load(string name)
    {
        name = name.Replace(" ", "_");

        string destination = SAVE_FOLDER + name + ".json";

        if (!File.Exists(destination))
        {
            Debug.LogWarning($"SaveLoadManager: File not found at {destination}");
            return default;
        }
        string jsonString = File.ReadAllText(destination, Encoding.UTF8);
        return jsonString;
    }

    public bool CheckIfFileExists(string path)
    {
        if(!File.Exists(path))
            return false;

        Debug.Log("File found at: " + path);

        string jsonString = File.ReadAllText(path, Encoding.UTF8);

        if(jsonString == null || jsonString == "")
        {
            Debug.LogWarning("File is empty");
            return false;
        }

        Debug.Log("File Object is Json");

        return true;
    }
}