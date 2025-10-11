using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "NavigateRandomDest", story: "[Enemy] goes to a Random Destination by [Speed] [RotationSpeed] and [Acceleration]", category: "Action", id: "cb760a94b637f77e963fecedc68168a2")]
public partial class NavigateRandomDestAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> RotationSpeed;
    [SerializeReference] public BlackboardVariable<float> Acceleration;

    private EnemyNavigationSystem _navigationSystem;

    protected override Status OnStart()
    {
        _navigationSystem = Enemy.Value.GetComponent<EnemyNavigationSystem>();

        if(Enemy.Value == null)
        {
            return Status.Failure;
        }

        _navigationSystem.SetRandomDestination(Speed, RotationSpeed, Acceleration);

        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

