using UnityEngine;
using System;
using UnityEngine.InputSystem;

// MVC: Model View Control

[Flags]
public enum AbilityFlag
{
    None = 0,
    MoveKeyboard = 1 << 0,// 0001
    MoveMouse = 1 << 1,// 0010
    Jump = 1 << 2,// 0100
    Dodge = 1 << 3,// 1000
    Attack = 1 << 4,
    Damage = 1 << 5,

    // 적 캐릭터
    Wander = 1 << 6,
    Trace = 1 << 7,
    Detect = 1 << 8
}


// 데이터 담당: 역할
// 1. Ability의 타입을 정한다
// 2. Ability의 타입에 맞게 생성한다
public abstract class AbilityData : ScriptableObject
{
    public abstract AbilityFlag Flag { get; }
    public abstract Ability CreateAbility(CharacterControl owner);
}

//abstract: 무조건 정의
//virtual: 옵션
public abstract class Ability
{
    public virtual void Activate(object obj = null) { }
    public virtual void Deactivate() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { } //  빠르게(물리연산속도)업데이트
    public virtual AbilityData Getdata()
    {
        return null;
    }
}

public abstract class Ability<D> : Ability where D : AbilityData
{
    public D data;
    protected CharacterControl owner;
    public Ability(D data, CharacterControl ow)
    {
        this.owner = ow;
        this.data = data;
    }

    public override AbilityData Getdata()
    {
        return (AbilityData)data;
    }
}
