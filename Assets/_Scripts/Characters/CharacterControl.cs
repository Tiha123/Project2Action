using System.Collections.Generic;
using UnityEngine;
using CustomInspector;
using UnityEngine.InputSystem;

// GAS(Game ability system)

// 캐릭터관리
public class CharacterControl : MonoBehaviour
{
    [HideInInspector] public AbilityControl abilityControl;
    public List<AbilityData> initialAbilities = new List<AbilityData>();

    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isArrived = true;

    public float isGroundedOffset = 1.1f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;


    void Awake()
    {
        if (TryGetComponent(out abilityControl) == false)
        {
            Debug.LogWarning("CharacterControl ] AbilityControl없음");
        }
        if (TryGetComponent(out rb) == false)
        {
            Debug.LogWarning("CharacterControl ] Rigidbody없음");
        }
        if (TryGetComponent(out animator) == false)
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
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, isGroundedOffset);
    }

    #region InputSystem
    public void OnMoveKeyboard(InputAction.CallbackContext ctx)
    {
        if(ctx.performed || ctx.canceled)
        {
            abilityControl.Activate(AbilityFlag.MoveKeyboard, ctx);
        }
    }


    public void OnMoveMouse(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            abilityControl.Activate(AbilityFlag.MoveMouse, ctx);
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            abilityControl.Activate(AbilityFlag.Jump, ctx);
        }
    }
    #endregion
}
