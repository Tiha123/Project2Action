using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/Move")]
public class AbilityMoveData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Move;
    public float movespeed=10f;
    public float rotatespeed=50f;

    public override Ability CreateAbility(Transform owner)
    {
        return new AbilityMove(owner, movespeed, rotatespeed);
    }
}
