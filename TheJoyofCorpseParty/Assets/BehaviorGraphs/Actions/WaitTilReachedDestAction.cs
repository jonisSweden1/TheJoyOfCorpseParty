using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitTilReachedDest", story: "[Self] wait until have arrived", category: "Action", id: "29322454a7dd0dc02975772769509ec3")]
public partial class WaitTilReachedDestAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyNavigationSystem> Self;

    protected override Status OnStart()
    {
        if (Self == null)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(Self.Value.CheckNavigationFinished())
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

