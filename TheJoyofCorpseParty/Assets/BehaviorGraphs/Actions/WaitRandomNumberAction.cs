using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitRandomNumber", story: "Self says wait between [From] and [To]", category: "Action", id: "863d51546e6c7cd1373f3b7bd812863c")]
public partial class WaitRandomNumberAction : Action
{
    [SerializeReference] public BlackboardVariable<float> From;
    [SerializeReference] public BlackboardVariable<float> To;
    private float _currentTime = 0;
    private float _randomTime = 0;

    protected override Status OnStart()
    {
        if(From == null && To == null)
        {
            return Status.Failure;
        }
        _randomTime = UnityEngine.Random.Range(From.Value, To.Value);

        _currentTime = 0;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime > _randomTime)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

