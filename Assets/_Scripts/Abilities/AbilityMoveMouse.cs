using UnityEngine;
using UnityEngine.AI;

public class AbilityMoveMouse : Ability<AbilityMoveMouseData>
{

    private Camera camera;
    private NavMeshPath path;
    private Vector3[] corners;
    int next;
    bool stopTrigger=false;
    
    Quaternion lookrot;
    Vector3 target;
    Vector3 direction;
    Vector3 finaltarget;
    float currentVelocity;

    public AbilityMoveMouse(AbilityMoveMouseData data, CharacterControl ow) : base(data, ow)
    {
        camera = Camera.main;
        path = new NavMeshPath();
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
        InputMouse();
        MoveAnimation();
    }

    void InputMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                SetDestination(hit.point);
            }
        }

    }

    void FollowPath()
    {
        if (corners == null || corners.Length <= 0 || data.isArrived == true)
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
        currentVelocity=Vector3.Distance(Vector3.zero,owner.rb.linearVelocity);
        Debug.Log(currentVelocity);

        if (Vector3.Distance(target, owner.transform.position) < data.stopDistance)
        {
            next++;
            if (next >= corners.Length)
            {
                data.isArrived = true;
                owner.rb.linearVelocity = Vector3.zero;
            }
        }
    }

    void MoveAnimation()
    {
        if (Vector3.Distance(finaltarget, owner.rb.position) < data.runtostopDistance&&data.isArrived==false&&stopTrigger==false)
        {
            owner.animator?.CrossFadeInFixedTime("RUNTOSTOP", 0.2f, 0, 0f);
            stopTrigger=true;
        }
        float a = data.isArrived ? 0f : Mathf.Clamp01(currentVelocity);
        float movespd = Mathf.Lerp(owner.animator.GetFloat("movespeed"), a, Time.deltaTime * 10f);
        owner.animator?.SetFloat("movespeed", movespd);
    }

    void SetDestination(Vector3 destination)
    {
        if (!NavMesh.CalculatePath(owner.transform.position, destination, NavMesh.AllAreas, path))
        {
            return;
        }
        corners = path.corners;
        next = 1;
        finaltarget=corners[corners.Length-1];
        data.isArrived = false;
        stopTrigger=false;
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
