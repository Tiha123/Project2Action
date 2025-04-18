using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;

public class AbilityPlayerAttack : Ability<AbilityPlayerAttackData>
{
    bool isAttacking = false;
    private CancellationTokenSource cts;
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
        data.eventAttackBefore.Register(OneventAttackBefore);

    }

        public override void Deactivate()
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.Attack.performed-=InputAttack;
        }
        cts.Cancel();
        cts.Dispose();
        data.eventAttackBefore.Unregister(OneventAttackBefore);
    }

        void InputAttack(InputAction.CallbackContext ctx)
    {
        if(isAttacking=true || ctx.performed==true)
        {
            return;
        }
        DelayAttack().Forget();
        AnimationClip aniclip=owner.Profile.ATTACK.Random();
        owner.AnimateTrigger("ATTACK", owner.Profile.animatorOverride, aniclip);
    }

    async UniTaskVoid DelayAttack()
    {
        try
        {
            isAttacking=true;
            await UniTask.WaitForSeconds(data.attackSpeed, cancellationToken: cts.Token);
            isAttacking=false;
        }
        catch(System.OperationCanceledException e)
        {
            //Debug.LogException(e);
        }
        catch(System.Exception e)
        {
            //Debug.LogException(e);
        }
    }

    public void OneventAttackBefore(EventAttackBefore e)
    {
        if(owner != e.from)
        {
            return;
        }
        Vector3 origin=owner.eyePoint.position;
        float range=owner.state.attackRange;
        Vector3 direction=owner.eyePoint.forward;
        int mask=LayerMask.GetMask("Actor");
        Collider[] cols = Physics.OverlapSphere(origin,range,mask);
        if(cols.ToList().Count<=0)
        {
            return;
        }
        foreach (var c in cols)
        {
            if(c.tag !="Enemy")
            {
                continue;
            }
            var cc = c.GetComponentInParent<CharacterControl>();
            if(cc=null)
            {
                return;
            }
            data.target=cc;
            data.eventAttackDamage.from=owner;
            data.eventAttackDamage.to=cc;
            data.eventAttackDamage.damage=owner.state.attackDamage;
            data.eventAttackDamage.Raise();
        }
    }
}
