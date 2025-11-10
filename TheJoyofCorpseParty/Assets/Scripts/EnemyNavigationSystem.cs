using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigationSystem : MonoBehaviour
{
    int previousDestinationIndex = -1;
    private NavMeshAgent m_Agent;

    [Header("List destinations")]
    [SerializeField]
    private Transform[] m_Destinations;

    [SerializeField]
    private Transform m_RootListDestinations;

    public Transform[] Destinations { get { return m_Destinations; } }


    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        m_Destinations = GetAllDestinations();
    }

    private Transform[] GetAllDestinations()
    {
        if (m_RootListDestinations != null)
        {
            List<Transform> destinations = new List<Transform>();

            foreach (Transform t in m_RootListDestinations)
            {
                destinations.Add(t);
            }

            return destinations.ToArray();
        }
        return null;
    }

    public bool CheckListDestinationsNotNull()
    {
        if (m_Destinations != null)
            return true;

        return false;
    }

    public void SetRandomDestination(float speed = 1, float angularSpeed = 720, float acceleration = 8)
    {
        m_Agent.speed = speed;
        m_Agent.angularSpeed = angularSpeed;
        m_Agent.acceleration = acceleration;

        int rndIndex = UnityEngine.Random.Range(0, Destinations.Length - 1);

        Debug.Log(rndIndex);

        if (previousDestinationIndex == rndIndex && previousDestinationIndex != -1)
        {
            if (rndIndex > Destinations.Length - 1)
            {
                rndIndex--;
            }
            else
            {
                rndIndex++;
            }
        }

        m_Agent.SetDestinationImmediate(
            Destinations[rndIndex].position,
            m_Agent.radius + m_Agent.stoppingDistance + m_Agent.height);

        previousDestinationIndex = rndIndex;
    }

    public void SetDestination(Transform dest, float speed = 1, float angularSpeed = 720, float acceleration = 8)
    {
        m_Agent.speed = speed;
        m_Agent.angularSpeed = angularSpeed;
        m_Agent.acceleration = acceleration;

        m_Agent.SetDestinationImmediate(
            dest.position, 
            m_Agent.radius + m_Agent.stoppingDistance + m_Agent.height);
    }

    public void SetDestination(Vector3 destPos, float speed = 1, float angularSpeed = 720, float acceleration=8)
    {
        m_Agent.speed = speed;
        m_Agent.angularSpeed = angularSpeed;
        m_Agent.acceleration = acceleration;

        m_Agent.SetDestinationImmediate(
            destPos,
            m_Agent.radius + m_Agent.stoppingDistance + m_Agent.height);
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
