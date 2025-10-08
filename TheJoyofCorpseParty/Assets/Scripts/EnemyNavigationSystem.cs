using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigationSystem : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Transform destinationRef, float speed = 1, float angularSpeed = 720, float acceleration = 8)
    {
        m_Agent.speed = speed;
        m_Agent.angularSpeed = angularSpeed;
        m_Agent.acceleration = acceleration;

        m_Agent.SetDestination(destinationRef.position);
    }

    public bool CheckNavigationFinished()
    {
        if (m_Agent.pathStatus == NavMeshPathStatus.PathComplete && m_Agent.remainingDistance <= m_Agent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    public void StopNavigating()
    {
        m_Agent.ResetPath();
    }
}
