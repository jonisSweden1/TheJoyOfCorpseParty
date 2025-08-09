using System;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    public bool m_IsAlive { get; private set; } = true;
    public event Action m_OnDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        m_OnDeath += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        m_OnDeath -= ShowGameOverScreen;
    }

    public void KillPlayer()
    {
        m_OnDeath?.Invoke();
        m_IsAlive=false;
    }

    void ShowGameOverScreen()
    {
        UIManager.instance.ForceMenu(1);
    }
}
