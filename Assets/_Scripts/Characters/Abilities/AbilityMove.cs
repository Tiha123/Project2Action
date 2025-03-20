using UnityEngine;

public class AbilityMove : Ability
{
    private float movespeed;
    private float rotatespeed;

    public override AbilityData Data => Data as AbilityMoveData;
    public AbilityMove(Transform owner, float movespd, float rotatespd) : base(owner)
    {
        cameraTransform=Camera.main.transform;
        this.movespeed=movespd;
        this.rotatespeed=rotatespd;
        rb=owner.GetComponentInChildren<Rigidbody>();
        if(rb==null)
        {
            Debug.LogError("AbilityMove ] Rigidbody 없음");
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
        InputKeyboard();
        Rotate();
        Movement();
    }

    float horz, vert;
    void InputKeyboard()
    {
        horz=Input.GetAxisRaw("Horizontal");
        vert=Input.GetAxisRaw("Vertical");
        camForward=cameraTransform.forward;
        camRight=cameraTransform.right;
        camForward.y=0;
        camRight.y=0;
        camForward.Normalize();
        camRight.Normalize();
        movement=(camForward*vert+camRight*horz).normalized;
    }
    Rigidbody rb;
    Transform cameraTransform;
    Vector3 movement;
    Vector3 camForward, camRight;
    Quaternion targetRotation;
    void Movement()
    {
        rb.linearVelocity=Vector3.Lerp(rb.linearVelocity, movement*movespeed, Time.deltaTime);
    }
    void Rotate()
    {
        if(movement!=Vector3.zero)
        {
            targetRotation=Quaternion.LookRotation(movement);
            rb.rotation=Quaternion.Slerp(rb.rotation, targetRotation, rotatespeed*Time.deltaTime);
        }
    }
}
