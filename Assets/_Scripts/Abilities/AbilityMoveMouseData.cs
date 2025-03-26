using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/MoveMouse")]
public class AbilityMoveMouseData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Move;

    public float movePerSec=10f;
    public float rotatePerSec=1080f;
    public float stopDistance=0.1f;
    public float runtostopDistance=1f;

    [Space(20)]
    public GameObject marker; // 3d 피킹 마커 오브젝트

    public override Ability CreateAbility(CharacterControl owner)
    {
        return new AbilityMoveMouse(this, owner);
    }
}
