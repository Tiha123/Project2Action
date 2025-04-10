using UnityEngine;

public class AbilityAttack : Ability<AbilityAttackData>
{
    private float attackSpeed;
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
        if (obj != null && obj is CharacterControl)
        {
            data.target = obj as CharacterControl;
        }
        owner.Display(data.Flag.ToString());
    }

    public override void Deactivate()
    {
    }

    float elapese = 0f;
    public override void Update()
    {
        if (data.target == null)
        {
            return;
        }

        elapese += Time.deltaTime;
        if (elapese >= attackSpeed)
        {
            owner.AnimateSinlgeAttack(data.target.eyePoint.position);
            elapese = 0f;
        }

        owner.AnimateMoveSpeed(0f);
    }

    
}