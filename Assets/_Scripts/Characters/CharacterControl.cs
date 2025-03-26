using System.Collections.Generic;
using UnityEngine;
using CustomInspector;
using UnityEngine.AI;

// GAS(Game ability system)

// 캐릭터관리
public class CharacterControl : MonoBehaviour
{
    [HideInInspector] public AbilityControl abilityControl;
    public List<AbilityData> initialAbilities = new List<AbilityData>();
    [ReadOnly] public bool isGrounded;
    public float isGroundedOffset=1.1f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;

    void Awake()
    {
        if(TryGetComponent(out abilityControl)==false)
        {
            Debug.LogWarning("CharacterControl ] AbilityControl없음");
        }
        if(TryGetComponent(out rb)==false)
        {
            Debug.LogWarning("CharacterControl ] Rigidbody없음");
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
        isGrounded=Physics.Raycast(transform.position+Vector3.up,Vector3.down,isGroundedOffset);
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
