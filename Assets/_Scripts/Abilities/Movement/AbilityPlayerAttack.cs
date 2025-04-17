using Project2Action;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityPlayerAttack : Ability<AbilityPlayerAttackData>
{
    public AbilityPlayerAttack(AbilityPlayerAttackData data, CharacterControl owner) : base(data, owner)
    {
        if (owner.Profile == null)
        {
            return;
        }
        data.attackSpeed=owner.Profile.attackSpeed;
    }

    public override void Activate(object obj = null)
    {
        if (owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.Attack.performed += InputAttack;
        }
    }

        public override void Deactivate()
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.Attack.performed-=InputAttack;
        }
    }

        void InputAttack(InputAction.CallbackContext ctx)
    {
        AnimationClip aniclip=owner.Profile.ATTACK.Random();
        owner.AnimateTrigger("ATTACK", owner.Profile.animatorOverride, aniclip);
    }
}
