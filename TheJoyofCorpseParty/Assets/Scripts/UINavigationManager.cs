using UnityEngine;
using UnityEngine.Events;

public class UINavigationManager : MonoBehaviour
{
    [SerializeField]
    private int _settingIndex = 0;

    private int _currentScreenIndex = 0;

    [SerializeField]
    private GameObject[] _uiNavigationScreens;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentScreenIndex = _settingIndex;
    }

    public void ChangeScreen(int screenIndex)
    {
        if (screenIndex >= 0 && screenIndex < _uiNavigationScreens.Length)
        {
            _currentScreenIndex = screenIndex;
            for (int i = 0; i < _uiNavigationScreens.Length; i++)
            {
                if (i == _currentScreenIndex)
                {
                    _uiNavigationScreens[i].SetActive(true);
                }
                else
                {
                    _uiNavigationScreens[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("Screen index out of range: " + screenIndex);
        }
    }
}
