using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    public bool m_IsAlive { get; private set; } = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KillPlayer()
    {
        m_IsAlive=false;
    }
}
