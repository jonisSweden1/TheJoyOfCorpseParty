using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private ASyncLoader _syncLoader;

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

    public void SetMessage(string message)
    {
        DataTransferToScene.messageData = message;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void LoadSceneAsync(string sceneName)
    {
        if(_syncLoader != null)
        {
            StartCoroutine(_syncLoader.LoadAsyncScene(sceneName));
        }
    }

    public void LoadSceneAsync(int sceneId)
    {
        if (_syncLoader != null)
        {
            StartCoroutine(_syncLoader.LoadAsyncScene(sceneId));
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart scene");
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
}
