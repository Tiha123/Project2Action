using System.Collections.Generic;
using CustomInspector;
using UnityEngine;


public enum ActorType {_None, Player, ENEMY, NPC}

[CreateAssetMenu(menuName = "Datas/ActorProfile")]
public class ActorProfile : ScriptableObject
{
    [HorizontalLine("Prefabs"), HideField] public bool _l0;
    public string alias;
    [Preview(Size.medium)] public Sprite portrait;
    [Preview(Size.medium)] public List<GameObject> models;
    [Preview(Size.medium)] public Avatar avatar;

    [HorizontalLine("Animation"), HideField] public bool _l3;
    public List<AnimationClip> ATTACK;
    public AnimatorOverrideController aoc;

    [HorizontalLine("Attributes"), HideField] public bool _l1;
    [Tooltip("체력")] public int health;
    [Tooltip("초당 이동속도")] public float movePerSec;
    [Tooltip("초당 회전속도")] public float rotatePerSec;
    [Tooltip("점프 강도")] public float jumpPower;
    [Tooltip("점프 시간")] public float jumpDuration;
    [Tooltip("초당 공격 속도(Attack/sec)")] public float attackSpeed;
    [Tooltip("공격 범위(/m)")] public float attackRange;
    [Tooltip("시야 범위(/m)")] public float sightRange;
    [Tooltip("기본 공격력")] public int baseDamage;
    public ActorType actorType;
    public AnimatorOverrideController animatorOverride;

    [HorizontalLine("Abilities"), HideField] public bool _l2;
    public List<AbilityData> initialAbilities = new List<AbilityData>();
}
