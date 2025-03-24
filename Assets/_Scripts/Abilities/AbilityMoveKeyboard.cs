using UnityEngine;

public class AbilityMoveKeyboard : Ability<AbilityMoveKeyboardData>
{
    float horz, vert;
    Transform cameraTransform;
    Vector3 direction;
    Vector3 camForward, camRight;
    float velocity;
    public AbilityMoveKeyboard(AbilityMoveKeyboardData data, CharacterControl owner) : base(data, owner)
    {
        cameraTransform = Camera.main.transform;
        velocity = data.rotatePerSec;
    }
    public override void FixedUpdate()
    {
        InputKeyboard();
        Rotate();
        Movement();
    }

    void InputKeyboard()
    {
        horz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        camForward = cameraTransform.forward;
        camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        direction = (camForward * vert + camRight * horz).normalized;
    }
    void Movement()
    {
        owner.cc.Move(direction * data.movePerSec * Time.deltaTime);
        if (owner.isGrounded==true)
        {
            float dist=Vector3.Distance(Vector3.zero, owner.cc.velocity);
            float targetspeed=Mathf.Clamp01(Mathf.Abs(dist)/data.movePerSec);
            float movespd=Mathf.Lerp(owner.animator.GetFloat("movespeed"), targetspeed, Time.deltaTime*10f);
            owner.animator?.SetFloat("movespeed",movespd );
        }
    }

    void Rotate()
    {
        if (direction == Vector3.zero)
        {
            return;
        }
        // Atan2: Vector2(x,z)가 있을 때 해당 각도를 알려준다(radian)
        // pie(π) (3.14) => 180 degree
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float smoothangle = Mathf.SmoothDampAngle(owner.transform.eulerAngles.y, angle, ref velocity, 0.1f);
        owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.x, smoothangle, owner.transform.rotation.z);
    }
}
