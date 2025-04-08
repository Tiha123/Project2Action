using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityMoveKeyboard : Ability<AbilityMoveKeyboardData>
{
    float horz, vert;
    Transform cameraTransform;
    Vector3 direction;
    Vector3 camForward, camRight;
    float _rotvel;
    CharacterControl ownerCC;

    
    public AbilityMoveKeyboard(AbilityMoveKeyboardData data, IActorControl owner) : base(data, owner)
    {
        ownerCC=(CharacterControl) owner;
        cameraTransform = Camera.main.transform;
        if(ownerCC.Profile==null)
        {
            return;
        }
        data.movePerSec=owner.Profile.movePerSec;
        data.rotatePerSec=owner.Profile.rotatePerSec;
    }

    
    public override void FixedUpdate()
    {
        Rotate();
        Movement();
    }

    public override void Activate()
    {
        ownerCC.actionInput.Player.Move.performed += InputMove;
        ownerCC.actionInput.Player.Move.canceled += InputStop;
    }

    public override void Deactivate()
    {
        ownerCC.actionInput.Player.Move.performed -= InputMove;
        ownerCC.actionInput.Player.Move.canceled -= InputStop;

        Stop();
    }

    void InputMove(InputAction.CallbackContext ctx)
    {
        ownerCC.isArrived=!ctx.performed;
        Vector2 move=ctx.ReadValue<Vector2>();

        horz=move.x;
        vert=move.y;

        camForward = cameraTransform.forward;
        camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        direction = (camForward * vert + camRight * horz).normalized;
    }

    void InputStop(InputAction.CallbackContext ctx)
    {
        ownerCC.isArrived=ctx.canceled;
        if(ctx.canceled)
        {
            Stop();
        }
    }
    
    void Stop()
    {
        direction=Vector3.zero;
        ownerCC.rb.linearVelocity = Vector3.zero;
        ownerCC.animator?.SetFloat(AnimatorHashSet._MOVESPEED, 0f);
    }

    void Movement()
    {
        Vector3 movement=direction * data.movePerSec * 50f * Time.deltaTime;
        Vector3 velocity=new Vector3(movement.x, ownerCC.rb.linearVelocity.y, movement.z);
        ownerCC.rb.linearVelocity = velocity; //movePerSec과 GetRelativeVelocity값을 동기화하기위한 상수
        if (ownerCC.isGrounded == true)
        {
            float velocity2 = Vector3.Distance(Vector3.zero, ownerCC.rb.linearVelocity);
            float targetspeed = Mathf.Clamp01(Mathf.Abs(velocity2) / data.movePerSec);
            float movespd = Mathf.Lerp(ownerCC.animator.GetFloat(AnimatorHashSet._MOVESPEED), targetspeed, Time.deltaTime * 10f);
            ownerCC.animator?.SetFloat(AnimatorHashSet._MOVESPEED, movespd);
            // if(horz==0f&&vert==0f)
            // {
            //     owner.animator?.CrossFadeInFixedTime("RUNTOSTOP", 0.2f, 0, 0f);
            // }
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
        float smoothangle = Mathf.SmoothDampAngle(ownerCC.transform.eulerAngles.y, angle, ref data.rotatePerSec, 0.1f);
        ownerCC.transform.rotation = Quaternion.Euler(ownerCC.transform.rotation.x, smoothangle, ownerCC.transform.rotation.z);
    }
}
