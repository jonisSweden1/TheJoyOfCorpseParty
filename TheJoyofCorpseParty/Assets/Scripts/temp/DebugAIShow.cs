using UnityEngine;
using UnityEngine.AI;

public class DebugAIShow : MonoBehaviour
{
    NavMeshAgent agent;
    LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        UpdatePath();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.hasPath)
        {
            UpdatePath();
        }
    }

    void UpdatePath()
    {
        NavMeshPath path = new NavMeshPath();
        if(agent.CalculatePath(agent.destination, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
