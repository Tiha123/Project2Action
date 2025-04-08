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

    CharacterControl ownerCC;

    Quaternion lookrot;
    Vector3 target;
    Vector3 direction;
    Vector3 finaltarget;
    float currentVelocity;
    private RaycastHit hitinfo;
    private ParticleSystem marker;

    public AbilityMoveMouse(AbilityMoveMouseData data, IActorControl ow) : base(data, ow)
    {
        ownerCC=(CharacterControl)owner;
        camera = Camera.main;
        path = new NavMeshPath();
        marker = GameObject.Instantiate(data.marker).GetComponent<ParticleSystem>();
        if (marker == null)
        {
            Debug.LogWarning("MoveMouse ] marker없음");
        }
        marker.gameObject.SetActive(false);
        if(ownerCC.Profile==null)
        {
            return;
        }
        data.movePerSec=owner.Profile.movePerSec;
        data.rotatePerSec=owner.Profile.rotatePerSec;
    }

    public override void Activate()
    {
        ownerCC.actionInput.Player.MoveMouse.performed+=InputMove;
        
    }

    public override void Deactivate()
    {
        ownerCC.actionInput.Player.MoveMouse.performed-=InputMove;
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
        if (ownerCC == null || ownerCC.rb == null)
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
        if (corners == null || corners.Length <= 0 || ownerCC.isArrived == true)
        {
            return;
        }
        target = corners[next];
        direction = (target - ownerCC.transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            lookrot = Quaternion.LookRotation(direction);
        }

        ownerCC.transform.rotation = Quaternion.RotateTowards(ownerCC.transform.rotation, lookrot, data.rotatePerSec * Time.deltaTime);

        Vector3 movement = direction * data.movePerSec * 50f * Time.deltaTime;
        ownerCC.rb.linearVelocity = new Vector3(movement.x, ownerCC.rb.linearVelocity.y, movement.z);
        currentVelocity = Vector3.Distance(Vector3.zero, ownerCC.rb.linearVelocity);

        if (Vector3.Distance(target, ownerCC.transform.position) < data.stopDistance)
        {
            next++;
            if (next >= corners.Length)
            {
                ownerCC.isArrived = true;
                ownerCC.rb.linearVelocity = Vector3.zero;
            }
        }
    }

    void MoveAnimation()
    {
        if (Vector3.Distance(finaltarget, ownerCC.rb.position) < data.runtostopDistance.x && ownerCC.isArrived == false && stopTrigger == false)
        {
            ownerCC.Animate(ownerCC._RUNTOSTOP, 0.2f);
            stopTrigger = true;
        }
        // else if (owner.isArrived == false && stopTrigger == true)
        // {
        //     stopTrigger = false;
        // }
        float a = ownerCC.isArrived ? 0f : Mathf.Clamp01(currentVelocity);
        float movespd = Mathf.Lerp(ownerCC.animator.GetFloat(ownerCC._MOVESPEED), a, Time.deltaTime * 10f);
        ownerCC.animator?.SetFloat(ownerCC._MOVESPEED, movespd);
    }

    void SetDestination(Vector3 destination)
    {
        if (!NavMesh.CalculatePath(ownerCC.transform.position, destination, NavMesh.AllAreas, path))
        {
            return;
        }
        corners = path.corners;
        next = 1;
        finaltarget = corners[corners.Length - 1];
        ownerCC.isArrived = false;
        stopTrigger = Vector3.Distance(ownerCC.rb.position, hitinfo.point) > data.runtostopDistance.y ? false : true;
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
