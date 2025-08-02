using System;
using UnityEngine;

public class EnemyDetectionSystem : MonoBehaviour
{
    public float fovSize;

    public float fovRange;

    public event Action onDetected;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    LayerMask obstacleMask;

    [SerializeField]
    float maxDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        Vector3 directionCalculation = _player.position - transform.position;

        Debug.Log(directionCalculation);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionCalculation, out hit, maxDistance, obstacleMask))
        {
            if(hit.collider.tag == "Player")
            {
                Debug.Log("Player found");
                onDetected.Invoke();
            }
        }
    }
}
