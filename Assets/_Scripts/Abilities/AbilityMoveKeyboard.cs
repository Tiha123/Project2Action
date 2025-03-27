using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class AbilityMoveKeyboard : Ability<AbilityMoveKeyboardData>
{
    float horz, vert;
    Transform cameraTransform;
    Vector3 direction;
    Vector3 camForward, camRight;
    float velocity;
    InputAction.CallbackContext context;
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

    public override void Activate(InputAction.CallbackContext context)
    {
        this.context=context;
        owner.isArrived=context.canceled;
    }

    void InputKeyboard()
    {
    
        Vector2 move=context.ReadValue<Vector2>();

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

    void Movement()
    {
        Vector3 movement=direction * data.movePerSec * 50f * Time.deltaTime;
        Vector3 velocity=new Vector3(movement.x, owner.rb.linearVelocity.y, movement.z);
        owner.rb.linearVelocity = velocity; //movePerSec과 GetRelativeVelocity값을 동기화하기위한 상수
        Debug.Log(owner.rb.linearVelocity);
        if (owner.isGrounded == true)
        {
            float velocity2 = Vector3.Distance(Vector3.zero, owner.rb.linearVelocity);
            float targetspeed = Mathf.Clamp01(Mathf.Abs(velocity2) / data.movePerSec);
            float movespd = Mathf.Lerp(owner.animator.GetFloat("movespeed"), targetspeed, Time.deltaTime * 10f);
            owner.animator?.SetFloat("movespeed", movespd);
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
        float smoothangle = Mathf.SmoothDampAngle(owner.transform.eulerAngles.y, angle, ref velocity, 0.1f);
        owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.x, smoothangle, owner.transform.rotation.z);
    }
}
