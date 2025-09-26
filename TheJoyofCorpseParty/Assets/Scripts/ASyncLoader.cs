using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject m_LoadingScreen;
    [SerializeField] private GameObject m_NightStartupScreen;

    [SerializeField] private Slider m_LoadingSlider;

    public IEnumerator LoadAsyncScene(int sceneId)
    {
        EnableLoadingScreen();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        while (!asyncLoad.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress);
            m_LoadingSlider.value = progressValue;

            yield return null;
        }
    }

    public IEnumerator LoadAsyncScene(string sceneName)
    {
        EnableLoadingScreen();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress);
            m_LoadingSlider.value = progressValue;

            yield return null;
        }
    }

    void EnableLoadingScreen()
    {
        m_NightStartupScreen.SetActive(false);
        m_LoadingScreen.SetActive(true);
    }
}
