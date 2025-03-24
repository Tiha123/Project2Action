using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/MoveMouse")]
public class AbilityMoveMouseData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Move;

    public float movePerSec=10f;
    public float rotatePerSec=50f;

    public override Ability CreateAbility(CharacterControl owner)
    {
        return new AbilityMoveMouse(this, owner);
    }
}
