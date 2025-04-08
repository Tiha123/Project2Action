using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName ="Abilities/Wander")]
public class AbilityWanderData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Wander;

    [ReadOnly] public float movePerSec=10f;
    [ReadOnly] public float rotatePerSec=1080f;
    public float stopDistance=0.1f;

    public override Ability CreateAbility(IActorControl owner) => new AbilityWander(this, owner);

[Tooltip("배회할 범위(radius)")]
    public float wanderRadius=5f;
    public float wanderStay=2f;
}
