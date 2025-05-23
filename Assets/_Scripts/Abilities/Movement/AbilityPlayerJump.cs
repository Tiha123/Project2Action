using Project2Action;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityJump : Ability<AbilityJumpData>
{
    private bool isJumping = false;

    public AbilityJump(AbilityJumpData data, CharacterControl owner) : base(data, owner)
    {
        if(owner.Profile==null)
        {
            return;
        }
        data.jumpForce=owner.Profile.jumpPower;
        data.jumpDuration=owner.Profile.jumpDuration;
    }

    public override void Activate(object obj=null)
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.Jump.performed += InputJump;
        }
        //owner.animator?.SetTrigger("JumpUp");
    }

    public override void Deactivate()
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.Jump.performed -= InputJump;
        }
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
            JumpDown();
        }
    }

    private void JumpUp()
    {
         if (owner.isGrounded == false || owner.rb == null || isJumping == true)
            {
                return;
            }
            elapsed = 0f;
            isJumping = true;
            owner.Animate(AnimatorHashSet.JUMPUP, 0.1f);
    }

    private void JumpDown()
    {
        isJumping = false;
        owner.Animate(AnimatorHashSet.JUMPDOWN, 0.02f); //jumpForce와 linearvelocity의 동기화
    }

    private void InputJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
           JumpUp();
        }
    }
}
