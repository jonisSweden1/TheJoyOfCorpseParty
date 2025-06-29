using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int _defaultMenuIndex;
    private int _previousMenuIndex = -1;
    private int _currentMenuIndex = -1;

    [SerializeField]
    public GameObject[] m_Menus;

    private void Start()
    {
        if (m_Menus != null)
        {
            m_Menus[_defaultMenuIndex].SetActive(true);
            _currentMenuIndex = _defaultMenuIndex;
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
            _previousMenuIndex = _currentMenuIndex;
        }
    }
}
