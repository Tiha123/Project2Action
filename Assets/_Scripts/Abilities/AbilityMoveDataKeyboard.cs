using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/MoveKeyboard")]
public class AbilityMoveKeyboardData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Move;

    public float movePerSec=10f;
    public float rotatePerSec=50f;

    public override Ability CreateAbility(CharacterControl owner)
    {
        return new AbilityMoveKeyboard(this, owner);
    }
}
