using UnityEngine;
using UnityEngine.AI;

public class DebugAIShow : MonoBehaviour
{
    NavMeshAgent agent;
    LineRenderer lineRenderer;

    private bool visualBool;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (visualBool)
            {
                lineRenderer.enabled = false;
                visualBool = false;
            }
            else
            {
                lineRenderer.enabled = true;
                visualBool = true;
            }
        }

        if(agent.hasPath && visualBool)
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
