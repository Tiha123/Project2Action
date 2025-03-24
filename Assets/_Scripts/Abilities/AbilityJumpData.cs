using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/Jump")]
public class AbilityJumpData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Jump;

    public float jumpForce=30f;

    public float jumpDuration=0.3f;

    public AnimationCurve jumpCurve;

    public override Ability CreateAbility(CharacterControl owner) => new AbilityJump(this, owner);
}
