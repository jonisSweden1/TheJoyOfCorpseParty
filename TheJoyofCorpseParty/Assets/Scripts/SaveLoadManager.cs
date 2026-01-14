using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Text;
using System.IO;

// JSON Save/Load Manager
public class SaveLoadManager : MonoBehaviour
{
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

    public void Save<T>(T objectToSave, string destination)
    {
        string jsonString = JsonUtility.ToJson(objectToSave, true);
        File.WriteAllText(destination, jsonString, Encoding.UTF8);
    }

    public T Load<T>(string destination)
    {
        if (!File.Exists(destination))
        {
            Debug.LogWarning($"SaveLoadManager: File not found at {destination}");
            return default;
        }
        string jsonString = File.ReadAllText(destination, Encoding.UTF8);
        T loadedObject = JsonUtility.FromJson<T>(jsonString);
        return loadedObject;
    }

    public bool CheckIfFileExists<T>(string path)
    {
        if(File.Exists(path))
            return false;

        string jsonString = File.ReadAllText(path, Encoding.UTF8);
        T objectToCheck = JsonUtility.FromJson<T>(jsonString);

        if (objectToCheck == null)
            return false;

        return true;
    }
}