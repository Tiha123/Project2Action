using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName ="Abilities/MoveKeyboard")]
public class AbilityMoveKeyboardData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.MoveKeyboard;

    [ReadOnly] public float movePerSec=10f;
    [ReadOnly] public float rotatePerSec=50f;

    public override Ability CreateAbility(CharacterControl owner)
    {
        return new AbilityMoveKeyboard(this, owner);
    }
}
