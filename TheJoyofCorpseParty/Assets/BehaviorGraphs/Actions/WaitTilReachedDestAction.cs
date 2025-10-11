using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitTilReachedDest", story: "[Self] wait until have arrived", category: "Action", id: "29322454a7dd0dc02975772769509ec3")]
public partial class WaitTilReachedDestAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    private EnemyNavigationSystem _enemyNavigationSystem;

    protected override Status OnStart()
    {
        if (Self.Value == null)
            return Status.Failure;

        _enemyNavigationSystem = Self.Value.GetComponent<EnemyNavigationSystem>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(_enemyNavigationSystem.CheckNavigationFinished())
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

