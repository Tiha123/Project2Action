using UnityEngine;

public class AbilityJump : Ability<AbilityJumpData>
{
    private bool isJumping=false;
    public AbilityJump(AbilityJumpData data, CharacterControl owner) : base(data, owner)
    {

    }

    public override void Activate()
    {
        if (owner.isGrounded == false || owner.cc == null)
        {
            return;
        }
        isJumping=true;
        owner.animator?.SetTrigger("JumpUp");
    }

    public override void Deactivate()
    {
        isJumping=false;
        elapsed=0f;
        owner.animator?.SetTrigger("JumpDown");
    }
    float elapsed=0f;
    float t;
    public override void Update()
    {
        if (owner.cc == null || isJumping==false)
        {
            return;
        }
        elapsed += Time.deltaTime;
        t = elapsed / data.jumpDuration;
        float height=data.jumpCurve.Evaluate(t) * data.jumpForce;
        owner.cc.Move(Vector3.up*height*Time.deltaTime);
        if(elapsed>=data.jumpDuration||(t>0.5f&&owner.isGrounded==true))
        {
            owner.abilityControl.Deactivate(data.Flag);
        }
    }
}
