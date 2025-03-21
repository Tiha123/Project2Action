using UnityEngine;

public class AbilityMove : Ability
{
    private float movespeed;
    private float rotatespeed;

    public override AbilityData Data => Data as AbilityMoveData;
    public AbilityMove(CharacterControl owner, float movespd, float rotatespd) : base(owner)
    {
        cameraTransform=Camera.main.transform;
        this.movespeed=movespd;
        this.rotatespeed=rotatespd;
    }
    public override void Update()
    {
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
    Transform cameraTransform;
    Vector3 movement;
    Vector3 camForward, camRight;
    Quaternion targetRotation;
    void Movement()
    {
       owner.rb.AddForce(movement*movespeed*Time.deltaTime);
    }
    void Rotate()
    {
        if(movement!=Vector3.zero)
        {
            targetRotation=Quaternion.LookRotation(movement);
            owner.transform.rotation=Quaternion.Slerp(owner.transform.rotation, targetRotation, rotatespeed*Time.deltaTime);
        }
    }
}
