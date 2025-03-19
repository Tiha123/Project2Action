using System;
using UnityEngine;

// GAS(Game ability system)

[Flags]
public enum Ability
{
    None=0,
    Move=1<<0,// 0001
    Jump=1<<1,// 0010
    Dodge=1<<2,// 0100
    Attack=1<<3// 1000
}

// 캐릭터관리
public class CharacterControl : MonoBehaviour
{
    [Space(10)]
    public Ability abilities;
    public Ability hasAbility;

    void Start()
    {
        abilities.Add(Ability.Move, () => GameManager.I.TriggerAbilityAdd(abilities));
    }

    void OnEnable()
    {
        GameManager.I.eventAbilityAdded += OnEventAbilityAdded;
    }

    void OnDisable()
    {
        GameManager.I.eventAbilityAdded -= OnEventAbilityAdded;
    }

    void OnEventAbilityAdded(Ability a)
    {
        var v=GetComponent<AbilityMove>();
        if(v!= null)
        {
            return;
        }
        gameObject.AddComponent<AbilityMove>();
    }
    // void OnEventUseAbility(Ability a)
    // {
    //     if(a.Has(Ability.Move))
    //     {
    //         Debug.Log($"어빌리티 사용 {a}");
    //     }
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     abilities.Add(Ability.Move);
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //     abilities.Remove(Ability.Move);
    // }

}
