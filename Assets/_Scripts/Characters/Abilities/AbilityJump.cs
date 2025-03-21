using UnityEngine;

public class AbilityJump : Ability
{
    private float jumpForce;

    public override AbilityData Data => Data as AbilityJumpData;
    public AbilityJump(CharacterControl owner, float jumpfrc) : base(owner)
    {
        jumpForce=jumpfrc;
    }

    public override void Activate()
    {
        // if (Data.Effect!=AbilityEffect.Instant)
        // {
        //     return;
        // }
        Debug.Log("점프발동");
        Jump();

    }
    // public override void Update()
    // {
    //     base.Update();
        // 
        // CustomGravity();
        //InputKeyboard();
        // Jump();

    // }

    void Jump()
    {
        if(owner.isGrounded==false)
        {
            return;
        }
        owner.rb.AddExplosionForce(jumpForce, owner.transform.position, 5f, 1f, ForceMode.Acceleration);
    }
}
