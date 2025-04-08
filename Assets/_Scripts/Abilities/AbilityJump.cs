using Project2Action;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityJump : Ability<AbilityJumpData>
{
    private bool isJumping = false;

    CharacterControl ownerCC;
    public AbilityJump(AbilityJumpData data, IActorControl owner) : base(data, owner)
    {
        ownerCC=((CharacterControl)owner);
        if(ownerCC.Profile==null)
        {
            return;
        }
        data.jumpForce=owner.Profile.jumpPower;
        data.jumpDuration=owner.Profile.jumpDuration;
    }

    public override void Activate()
    {
        ownerCC.actionInput.Player.Jump.performed += InputJump;
        //owner.animator?.SetTrigger("JumpUp");
    }

    public override void Deactivate()
    {

        ownerCC.actionInput.Player.Jump.performed -= InputJump;
        //owner.animator?.SetTrigger("JumpDown");
    }
    float elapsed = 0f;
    float t;
    public override void FixedUpdate()
    {
        if (ownerCC.rb == null || isJumping == false)
        {
            return;
        }
        elapsed += Time.deltaTime;
        t = Mathf.Clamp01(elapsed / data.jumpDuration);

        Vector3 velocity = ownerCC.rb.linearVelocity;
        velocity.y = data.jumpCurve.Evaluate(t) * data.jumpForce;
        ownerCC.rb.linearVelocity = velocity;

        if (t > 0.3f && ownerCC.isGrounded)
        {
            JumpDown();
        }
    }

    private void JumpUp()
    {
         if (ownerCC.isGrounded == false || ownerCC.rb == null || isJumping == true)
            {
                return;
            }
            elapsed = 0f;
            isJumping = true;
            ownerCC.Animate(AnimatorHashSet._JUMPUP, 0.1f);
    }

    private void JumpDown()
    {
        isJumping = false;
        ownerCC.Animate(AnimatorHashSet._JUMPDOWN, 0.02f); //jumpForce와 linearvelocity의 동기화
    }

    private void InputJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
           JumpUp();
        }
    }
}
