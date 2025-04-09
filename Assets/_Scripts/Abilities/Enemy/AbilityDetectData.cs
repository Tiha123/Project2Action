using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Detect")]
public class AbilityDetectData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Wander;
    public EventEnemyDetect eventEnemyDetect;

    public override Ability CreateAbility(CharacterControl owner) => new AbilityDetect(this, owner);
}
