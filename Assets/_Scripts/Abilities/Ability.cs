using UnityEngine;
using System;
using UnityEngine.InputSystem;

// MVC: Model View Control

[Flags]
public enum AbilityFlag
{
    None = 0,
    MoveKeyboard = 1 << 0,// 0001
    MoveMouse = 1 << 1,
    Jump = 1 << 2,// 0010
    Dodge = 1 << 3,// 0100
    Attack = 1 << 4// 1000
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
    public virtual void Activate(InputAction.CallbackContext context) { }
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
        this.data=data;
    }

    public override AbilityData Getdata()
    {
        return (AbilityData)data;
    }
}
