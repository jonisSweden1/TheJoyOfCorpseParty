using UnityEngine;

public class DestinationsVisualizer : MonoBehaviour
{
    private bool toggleVisual = false;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<MeshRenderer>().enabled = toggleVisual;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleVisuals();
        }
    }

    void ToggleVisuals()
    {
        if(toggleVisual)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }

            toggleVisual = false;
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }

            toggleVisual = true;
        }
    }
}
