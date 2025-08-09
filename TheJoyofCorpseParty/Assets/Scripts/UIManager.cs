using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField]
    private int _defaultMenuIndex;
    private int _previousMenuIndex = -1;
    private int _currentMenuIndex = -1;

    [SerializeField]
    private bool _showMenu = false;

    [SerializeField]
    public GameObject[] m_Menus;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (m_Menus != null)
        {
            if (_showMenu)
            {
                m_Menus[_defaultMenuIndex].SetActive(true);
                _currentMenuIndex = _defaultMenuIndex;
            }
        }
    }

    public void GoToMenu(int index)
    {
        if(m_Menus != null)
        {
            _previousMenuIndex = _currentMenuIndex;
            _currentMenuIndex = index;

            m_Menus[_previousMenuIndex].SetActive(false);
            m_Menus[index].SetActive(true);
        }
    }

    public void GoBack()
    {
        if(m_Menus != null)
        {
            m_Menus[_currentMenuIndex].SetActive(false);
            m_Menus[_previousMenuIndex].SetActive(true);
            int tempPrevious = _previousMenuIndex;
            _previousMenuIndex = _currentMenuIndex;
            _currentMenuIndex = tempPrevious;
        }
    }

    public void ForceMenu(int index)
    {
        Debug.Log("Force menu");

        if(_currentMenuIndex > -1)
        {
            m_Menus[_currentMenuIndex].SetActive(false);
        }
        m_Menus[index].SetActive(true);
    }

    public void CloseApplication()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
