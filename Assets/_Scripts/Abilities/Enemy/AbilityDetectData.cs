using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Detect")]
public class AbilityDetectData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Detect;
    public EventSensorTargetEnter eventSensorTargetEnter;

    public override Ability CreateAbility(CharacterControl owner) => new AbilityDetect(this, owner);
}
