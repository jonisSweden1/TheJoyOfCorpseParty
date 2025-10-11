using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/PlayerDetectedEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "PlayerDetectedEvent", message: "Self detected player", category: "Events", id: "f257370ba231948c6ea211b2f980362d")]
public sealed partial class PlayerDetectedEvent : EventChannel { }

