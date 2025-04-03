using System.Collections.Generic;
using CustomInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/ActorProfile")]

public class ActorProfile : ScriptableObject
{
    public string alias;
    [Preview(Size.medium)] public Sprite portrait;
    [Preview(Size.medium)] public GameObject model;
    [Preview(Size.medium)] public Avatar avatar;
    public List<AbilityData> initialAbilities = new List<AbilityData>();
}
