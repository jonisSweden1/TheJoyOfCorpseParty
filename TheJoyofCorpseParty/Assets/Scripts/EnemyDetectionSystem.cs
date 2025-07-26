using System;
using UnityEngine;

public class EnemyDetectionSystem : MonoBehaviour
{
    public event Action onDetected;

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, obstacleMask))
        {
            if(hit.collider.tag == "Player")
            {
                onDetected.Invoke();
            }
        }
    }
}
