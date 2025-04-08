using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName ="Abilities/Wander")]
public class AbilityWanderData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Wander;

    public override Ability CreateAbility(IActorControl owner) => new AbilityWander(this, owner);
}
