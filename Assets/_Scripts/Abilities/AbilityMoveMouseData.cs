using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName ="Abilities/MoveMouse")]
public class AbilityMoveMouseData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.MoveMouse;

    public float movePerSec=10f;
    public float rotatePerSec=1080f;
    public float stopDistance=0.1f;

    [Tooltip("min: runtostop모션 발동지점, max: runtostop 발동할 거리")]
    [AsRange(0f,10) ]public Vector2 runtostopDistance;

    [Space(20)]
    public GameObject marker; // 3d 피킹 마커 오브젝트

    public override Ability CreateAbility(CharacterControl owner)
    {
        return new AbilityMoveMouse(this, owner);
    }
}
