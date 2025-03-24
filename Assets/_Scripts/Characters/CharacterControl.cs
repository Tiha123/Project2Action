using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

// GAS(Game ability system)

// 캐릭터관리
public class CharacterControl : MonoBehaviour
{
    [HideInInspector] public AbilityControl abilityControl;
    public List<AbilityData> initialAbilities = new List<AbilityData>();
    [ReadOnly] public bool isGrounded;
    [HideInInspector] public CharacterController cc;
    [HideInInspector] public Animator animator;

    void Awake()
    {
        if(TryGetComponent(out abilityControl)==false)
        {
            Debug.LogWarning("CharacterControl ] AbilityControl없음");
        }
        if(TryGetComponent(out cc)==false)
        {
            Debug.LogWarning("CharacterControl ] CharacterController없음");
        }
        if(TryGetComponent(out animator)==false)
        {
            Debug.LogWarning("CharacterControl ] Animator없음");
        }
    }

    void Start()
    {
        foreach (var dat in initialAbilities)
        {
            abilityControl.AddAbility(dat, true);
        }
    }

    void Update()
    {
        isGrounded=Physics.Raycast(transform.position+Vector3.up,Vector3.down,1.1f);
        InputKeyboard();
    }
    void InputKeyboard()
    {
        
        if(Input.GetButtonDown("Jump"))
        {
            abilityControl.Activate(AbilityFlag.Jump);
        }

    }
}
