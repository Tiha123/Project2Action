using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AbilityMoveMouse : Ability<AbilityMoveMouseData>
{

    private Camera camera;
    private NavMeshPath path;
    private Vector3[] corners;
    int next;
    bool stopTrigger = false;

    Quaternion lookrot;
    Vector3 target;
    Vector3 direction;
    Vector3 finaltarget;
    float currentVelocity;
    private RaycastHit hitinfo;
    private ParticleSystem marker;

    public AbilityMoveMouse(AbilityMoveMouseData data, CharacterControl ow) : base(data, ow)
    {
        camera = Camera.main;
        path = new NavMeshPath();
        marker = GameObject.Instantiate(data.marker).GetComponent<ParticleSystem>();
        if (marker == null)
        {
            Debug.LogWarning("MoveMouse ] marker없음");
        }
        marker.gameObject.SetActive(false);
        if(owner.Profile==null)
        {
            return;
        }
        data.movePerSec=owner.Profile.movePerSec;
        data.rotatePerSec=owner.Profile.rotatePerSec;
    }

    public override void Activate(object obj=null)
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.MoveMouse.performed+=InputMove;
        }
    }

    public override void Deactivate()
    {
        if(owner.TryGetComponent<InputControl>(out var input))
        {
            input.actionInput.Player.MoveMouse.performed-=InputMove;
        }
    }

    void InputMove(InputAction.CallbackContext ctx)
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hitinfo))
        {
            marker.gameObject.SetActive(true);
            marker.transform.position = hitinfo.point + Vector3.up * 0.2f;
            SetDestination(hitinfo.point);
            marker.Play();
        }
    }

    public override void FixedUpdate()
    {
        if (owner == null || owner.rb == null)
        {
            return;
        }
        FollowPath();
    }

    public override void Update()
    {
        MoveAnimation();
    }


    void FollowPath()
    {
        if (corners == null || corners.Length <= 0 || owner.isArrived == true)
        {
            return;
        }
        target = corners[next];
        direction = (target - owner.transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            lookrot = Quaternion.LookRotation(direction);
        }

        owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, lookrot, data.rotatePerSec * Time.deltaTime);

        Vector3 movement = direction * data.movePerSec * 50f * Time.deltaTime;
        owner.rb.linearVelocity = new Vector3(movement.x, owner.rb.linearVelocity.y, movement.z);
        currentVelocity = Vector3.Distance(Vector3.zero, owner.rb.linearVelocity);

        if (Vector3.Distance(target, owner.transform.position) < data.stopDistance)
        {
            next++;
            if (next >= corners.Length)
            {
                owner.isArrived = true;
                owner.rb.linearVelocity = Vector3.zero;
            }
        }
    }

    void MoveAnimation()
    {
        if (Vector3.Distance(finaltarget, owner.rb.position) < data.runtostopDistance.x && owner.isArrived == false && stopTrigger == false)
        {
            owner.Animate(AnimatorHashSet._RUNTOSTOP, 0.2f);
            stopTrigger = true;
        }
        if(stopTrigger==true&&owner.animator.GetHashCode()==AnimatorHashSet._RUNTOSTOP)
        {
            owner.Animate(AnimatorHashSet._LOCOMOTION, 0.1f);
        }
        float a = owner.isArrived ? 0f : Mathf.Clamp01(currentVelocity);
        float movespd = Mathf.Lerp(owner.animator.GetFloat(AnimatorHashSet._MOVESPEED), a, Time.deltaTime * 10f);
        owner.animator?.SetFloat(AnimatorHashSet._MOVESPEED, movespd);
    }

    void SetDestination(Vector3 destination)
    {
        if (!NavMesh.CalculatePath(owner.transform.position, destination, NavMesh.AllAreas, path))
        {
            return;
        }
        corners = path.corners;
        next = 1;
        finaltarget = corners[corners.Length - 1];
        owner.isArrived = false;
        stopTrigger = Vector3.Distance(owner.rb.position, hitinfo.point) > data.runtostopDistance.y ? false : true;
        DrawDebugPath();
    }

    private void DrawDebugPath()
    {
        if (corners == null)
        {
            return;
        }
        for (int i = 0; i < corners.Length - 1; ++i)
        {
            Debug.DrawLine(corners[i], corners[i + 1], Color.blue, 0.4f);
        }
    }
}
