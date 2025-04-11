using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

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

        cts=new CancellationTokenSource();
        attackSpeed = owner.Profile.attackSpeed;
    }

    public override void Activate(object obj = null)
    {
        if (obj != null && obj is CharacterControl)
        {
            data.target = obj as CharacterControl;
        }
        owner.Display(data.Flag.ToString());
    }

    public override void Deactivate()
    {
        cts.Cancel();
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
            await UniTask.WaitForSeconds(attackSpeed);
            isAttacking=false;
        }
        catch(System.Exception e)
        {
            Debug.LogException(e);
        }

    }


}