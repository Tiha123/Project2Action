using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityMoveKeyboard : Ability<AbilityMoveKeyboardData>
{
    float horz, vert;
    Transform cameraTransform;
    Vector3 direction;
    Vector3 camForward, camRight;
    float _rotvel;

    
    public AbilityMoveKeyboard(AbilityMoveKeyboardData data, CharacterControl owner) : base(data, owner)
    {
        cameraTransform = Camera.main.transform;
        if(owner.Profile==null)
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

    public override void Activate(object obj=null)
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.Move.performed += InputMove;
            input.actionInput.Player.Move.canceled += InputStop;
        }
    }

    public override void Deactivate()
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.Move.performed -= InputMove;
            input.actionInput.Player.Move.canceled -= InputStop;
        }

        Stop();
    }

    void InputMove(InputAction.CallbackContext ctx)
    {
        owner.isArrived=!ctx.performed;
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
        owner.isArrived=ctx.canceled;
        if(ctx.canceled)
        {
            Stop();
        }
    }
    
    void Stop()
    {
        direction=Vector3.zero;
        owner.rb.linearVelocity = Vector3.zero;
        owner.animator?.SetFloat(AnimatorHashSet.MOVESPEED, 0f);
    }

    void Movement()
    {
        Vector3 movement=direction * data.movePerSec * 50f * Time.deltaTime;
        Vector3 velocity=new Vector3(movement.x, owner.rb.linearVelocity.y, movement.z);
        owner.rb.linearVelocity = velocity; //movePerSec과 GetRelativeVelocity값을 동기화하기위한 상수
        if (owner.isGrounded == true)
        {
            float velocity2 = Vector3.Distance(Vector3.zero, owner.rb.linearVelocity);
            float targetspeed = Mathf.Clamp01(Mathf.Abs(velocity2) / data.movePerSec);
            float movespd = Mathf.Lerp(owner.animator.GetFloat(AnimatorHashSet.MOVESPEED), targetspeed, Time.deltaTime * 10f);
            owner.animator?.SetFloat(AnimatorHashSet.MOVESPEED, movespd);
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
        float smoothangle = Mathf.SmoothDampAngle(owner.transform.eulerAngles.y, angle, ref data.rotatePerSec, 0.1f);
        owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.x, smoothangle, owner.transform.rotation.z);
    }
}
