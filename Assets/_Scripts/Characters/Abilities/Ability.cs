using UnityEngine;
using System;

// MVC: Model View Control

[Flags]
public enum AbilityFlag
{
    None = 0,
    Move = 1 << 0,// 0001
    Jump = 1 << 1,// 0010
    Dodge = 1 << 2,// 0100
    Attack = 1 << 3// 1000
}

public enum AbilityEffect
{
    Instant,
    Duration,
    Infinite
}

// 데이터 담당: 역할
// 1. Ability의 타입을 정한다
// 2. Ability의 타입에 맞게 생성한다
public abstract class AbilityData : ScriptableObject
{
    public abstract AbilityFlag Flag { get; }
    public AbilityEffect Effect;
    public abstract Ability CreateAbility(CharacterControl owner);
}

//abstract: 무조건 정의
//virtual: 옵션
public abstract class Ability
{
    public abstract AbilityData Data { get; }
    protected CharacterControl owner;
    public Ability(CharacterControl ow) => this.owner = ow;
    public virtual void Activate() { }
    public virtual void Deactivate() { }
    public virtual void Update() { }
}
