using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPosition;

    private void OnEnable()
    {
        PlayerDeathManager.m_OnDeath += PlayerDeathManager_m_OnDeath;
    }

    private void OnDisable()
    {
        PlayerDeathManager.m_OnDeath -= PlayerDeathManager_m_OnDeath;
    }

    private void PlayerDeathManager_m_OnDeath()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerDeathManager.m_IsAlive)
        {
            transform.position = cameraPosition.position;
        }
    }
}
