using CustomInspector;
using UnityEngine;
[CreateAssetMenu(menuName ="Abilities/Attack")]
public class AbilityAIAttackData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Attack;

    public override Ability CreateAbility(CharacterControl owner)=>new AbilityAIAttack(this, owner);

    [ReadOnly] public CharacterControl target;

    public EventAttackBefore eventAttackBefore;
    public EventAttackDamage eventAttackDamage;

}
