using UnityEngine;
using UnityEngine.Events;

public class TimerToInvoke : MonoBehaviour
{
    private float m_Time = 0;

    [SerializeField]
    private bool invokeOnce = true;

    [SerializeField]
    private float m_Duration;

    private bool hasRunned = false;

    [SerializeField]
    private UnityEvent Event;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Time >= m_Duration)
        {
            if(Event != null)
            {
                //Invoke once
                if (invokeOnce)
                {
                    if(!hasRunned)
                    {
                        Event.Invoke();
                        hasRunned = true;
                    }
                }

                //Invoke per frame
                else
                {
                    Event.Invoke();
                }
            }
        }
        else
        {
            m_Time += Time.deltaTime;
        }
    }
}
