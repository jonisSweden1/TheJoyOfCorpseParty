using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DetectPlayer", story: "[Enemy] Detects Player", category: "Action", id: "d7d7ff934498b939cbc238fdfd4789c6")]
public partial class DetectPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyNavigationSystem> Enemy;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

