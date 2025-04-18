using CustomInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Damage")]
public class AbilityDamageData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Attack;

    public override Ability CreateAbility(CharacterControl owner) => new AbilityDamage(this, owner);

    public EventDeath eventDeath;

}
