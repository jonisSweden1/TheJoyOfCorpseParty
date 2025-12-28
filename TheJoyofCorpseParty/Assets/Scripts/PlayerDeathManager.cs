using JetBrains.Annotations;
using System;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    public static bool m_IsAlive { get; private set; } = true;
    public static event Action m_OnDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_IsAlive = true;
    }

    public void KillPlayer()
    {
        m_OnDeath?.Invoke();
        m_IsAlive=false;
    }
}
