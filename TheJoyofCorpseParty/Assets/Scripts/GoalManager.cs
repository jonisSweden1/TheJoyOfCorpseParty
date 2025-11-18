using System;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField]
    private Goal[] goals;

    public static GoalManager instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class Goal
{
    public string name;

    public Objective[] objectives;
}

[Serializable]
public class Objective
{
    public string name;

    [TextArea]
    public string instruction;
}
