using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChasePlayer", story: "[Enemy] Chase [Player] by [Speed] [RotationSpeed] and [Acceleration]", category: "Action", id: "cc125bd84ff4da7de94256c4da3cba06")]
public partial class ChasePlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> RotationSpeed;
    [SerializeReference] public BlackboardVariable<float> Acceleration;

    private EnemyNavigationSystem navigationSystem;
    private EnemyVisionDetection visionDetection;
    private Transform _player;

    protected override Status OnStart()
    {
        if (Player.Value == null)
            return Status.Failure;

        if (Enemy == null)
            return Status.Failure;
        _player = Player.Value;
        navigationSystem = Enemy.Value.GetComponent<EnemyNavigationSystem>();
        visionDetection = Enemy.Value.GetComponent<EnemyVisionDetection>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!visionDetection.CanSeePlayer)
            return Status.Success;

        navigationSystem.SetDestination(_player, Speed.Value, RotationSpeed.Value, Acceleration.Value);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

