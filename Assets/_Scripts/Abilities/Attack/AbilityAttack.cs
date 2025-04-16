using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class AbilityAttack : Ability<AbilityAttackData>
{
    bool isAttacking = false;
    private float attackSpeed;
    private CancellationTokenSource cts;

    public AbilityAttack(AbilityAttackData data, CharacterControl ow) : base(data, ow)
    {
        if (owner.Profile == null)
        {
            return;
        }

        attackSpeed = owner.Profile.attackSpeed;
    }


    public override void Activate(object obj = null)
    {
        cts?.Dispose();
        cts=new CancellationTokenSource();
        if (obj != null && obj is CharacterControl)
        {
            data.target = obj as CharacterControl;
        }
        owner.ui.Display(data.Flag.ToString());

        data.eventAttackBefore.Register(OneventAttackBefore);
    }


    public override void Deactivate()
    {
        data.eventAttackBefore.Unregister(OneventAttackBefore);
        cts.Cancel();
        cts.Dispose();
    }

    public void OneventAttackBefore(EventAttackBefore e)
    {
        if(owner != e.from)
        {
            return;
        }
        data.eventAttackDamage.from=owner;
        data.eventAttackDamage.to=data.target;
        data.eventAttackDamage.damage=owner.state.attackDamage;
        data.eventAttackDamage.Raise();
    }
    public override void Update()
    {
        if (isAttacking == true || data.target == null)
        {
            return;
        }

        DelayAttack().Forget();

        owner.LookatY(data.target.eyePoint.position);
        AnimationClip clip=owner.Profile.ATTACK.Random();

        owner.Animate("ATTACK", owner.Profile.aoc, clip, attackSpeed, 0.1f, 0);
        owner.AnimateMoveSpeed(0f,true);
    }

    async UniTaskVoid DelayAttack()
    {
        try
        {
            isAttacking=true;
            await UniTask.WaitForSeconds(attackSpeed, cancellationToken: cts.Token);
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
}