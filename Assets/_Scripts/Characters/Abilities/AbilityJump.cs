using UnityEngine;

public class AbilityJump : Ability
{
    private float jumpForce;
    private bool isGrounded;
    private bool jump;
    private float gravityScale;
    Rigidbody rb;

    public override AbilityData Data => Data as AbilityJumpData;
    public AbilityJump(Transform owner, float jumpfrc) : base(owner)
    {
        jumpForce=jumpfrc;
        isGrounded=false;
        jump=false;
        gravityScale=5f;
        rb=owner.GetComponentInChildren<Rigidbody>();
        if(rb==null)
        {
            Debug.LogError("AbilityJump ] Rigidbody 없음");
        }
    }

    public override void Activate()
    {
        base.Activate();
    }
    public override void Deactivate()
    {
        base.Deactivate();
    }
    public override void Update()
    {
        base.Update();
        isGrounded=Physics.Raycast(owner.position+Vector3.up,Vector3.down,1.1f);
        CustomGravity();
        InputKeyboard();
        Jump();

    }

    void InputKeyboard()
    {
        jump=Input.GetButtonDown("Jump");
    }

    void Jump()
    {
        if (jump&&isGrounded)
        {
            rb.AddExplosionForce(jumpForce, owner.transform.position, 5f);
        }
    }
    void CustomGravity()
    {
        rb.useGravity=isGrounded;
        if(!isGrounded)
        {
            rb.AddForce(Physics.gravity*gravityScale,ForceMode.Acceleration);
        }
    }
}
