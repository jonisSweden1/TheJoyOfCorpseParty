using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "NavigateRandomDest", story: "[Enemy] goes to a Random Destination by [Speed] [RotationSpeed] and [Acceleration]", category: "Action", id: "cb760a94b637f77e963fecedc68168a2")]
public partial class NavigateRandomDestAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyNavigationSystem> Enemy;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> RotationSpeed;
    [SerializeReference] public BlackboardVariable<float> Acceleration;
    protected override Status OnStart()
    {
        if(Enemy.Value == null)
        {
            return Status.Failure;
        }

        Enemy.Value.SetRandomDestination(Speed, RotationSpeed, Acceleration);

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

