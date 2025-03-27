using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityJump : Ability<AbilityJumpData>
{
    private bool isJumping = false;
    public AbilityJump(AbilityJumpData data, CharacterControl owner) : base(data, owner)
    {

    }

    public override void Activate(InputAction.CallbackContext ctx)
    {
        if (owner.isGrounded == false || owner.rb == null || isJumping == true)
        {
            return;
        }
        isJumping = true;
        owner.animator.CrossFadeInFixedTime("JUMPUP", 0.1f, 0, 0f);
        //owner.animator?.SetTrigger("JumpUp");
    }

    public override void Deactivate()
    {
        owner.animator.CrossFadeInFixedTime("JUMPDOWN", 0.02f, 0, 0f); //jumpForce와 linearvelocity의 동기화
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
        elapsed += Time.deltaTime;
        t = Mathf.Clamp01(elapsed / data.jumpDuration);

        Vector3 velocity = owner.rb.linearVelocity;
        velocity.y = data.jumpCurve.Evaluate(t) * data.jumpForce;
        owner.rb.linearVelocity = velocity;

        if (t > 0.3f && owner.isGrounded)
        {
            owner.abilityControl.Deactivate(data.Flag);
        }
    }
}
