using UnityEngine;

public class AbilityJump : Ability<AbilityJumpData>
{
    private bool isJumping = false;
    public AbilityJump(AbilityJumpData data, CharacterControl owner) : base(data, owner)
    {

    }

    public override void Activate()
    {
        if (owner.isGrounded == false || owner.rb == null || isJumping ==true)
        {
            return;
        }
        isJumping = true;
        owner.animator.CrossFadeInFixedTime("JUMPUP", 0.2f, 0, 0f);
        //owner.animator?.SetTrigger("JumpUp");
    }

    public override void Deactivate()
    {
        elapsed = 0f;
        isJumping = false;
        //owner.animator?.SetTrigger("JumpDown");
    }
    float elapsed = 0f;
    float t;
    public override void FixedUpdate()
    {
        if (owner.rb == null || isJumping == false)
        {
            return;
        }
        if (elapsed < data.jumpDuration)
        {
            elapsed += Time.deltaTime;
            t = elapsed / data.jumpDuration;
            float height = data.jumpCurve.Evaluate(t) * data.jumpForce;

            Vector3 velocity = owner.rb.linearVelocity;
            velocity.y = height * Time.deltaTime;
            owner.rb.linearVelocity = velocity * 800; //jumpForce와 linearvelocity의 동기화
        }
        else if (owner.isLanding == true)
        {
                owner.abilityControl.Deactivate(data.Flag);
        }
    }
}
