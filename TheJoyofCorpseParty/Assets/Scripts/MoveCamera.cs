using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        if(PlayerDeathManager.m_IsAlive)
        {
            transform.position = cameraPosition.position;
        }
    }
}
