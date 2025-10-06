using System;
using System.Collections;
using UnityEngine;

public class EnemyDetectionSystem : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject PlayerRef {  get; private set; }

    [SerializeField]
    private LayerMask targetMask, obstacleMask;

    public bool CanSeePlayer { get; private set; } = false;

    public event Action m_OnDetected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;

        while (true)
        {
            yield return new WaitForSeconds(delay);
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                Debug.Log("Sees player");

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                RaycastHit hit;

                if (Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, obstacleMask))
                {
                    Debug.Log(hit.collider);
                }

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    Debug.Log(hit.collider);

                    if(CanSeePlayer == false)
                    {
                        m_OnDetected.Invoke();
                        CanSeePlayer = true;
                    }
                }
                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }
}
