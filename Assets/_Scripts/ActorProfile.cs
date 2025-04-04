using System.Collections.Generic;
using CustomInspector;
using UnityEngine;

public enum ActorType {None, Player, NPC, Enemy, Boss, Item }

[CreateAssetMenu(menuName = "Datas/ActorProfile")]
public class ActorProfile : ScriptableObject
{
    [HorizontalLine("Prefabs"), HideField] public bool _l0;
    public ActorType actorType;
    public string alias;
    [Preview(Size.medium)] public Sprite portrait;
    [Preview(Size.medium)] public GameObject model;
    [Preview(Size.medium)] public Avatar avatar;

    [HorizontalLine("Attributes"), HideField] public bool _l1;
    [Tooltip("체력")] public int health;
    [Tooltip("초당 이동속도")] public float movePerSec;
    [Tooltip("초당 회전속도")] public float rotatePerSec;
    [Tooltip("점프 강도")] public float jumpPower;
    [Tooltip("점프 시간")] public float jumpDuration;

    [HorizontalLine("Abilities"), HideField] public bool _l2;
    public List<AbilityData> initialAbilities = new List<AbilityData>();
}
