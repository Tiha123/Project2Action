using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AbilityWander : Ability<AbilityWanderData>
{
    private Camera camera;
    private NavMeshPath path;
    private Vector3[] corners;
    int next;
    Quaternion lookrot;
    Vector3 target;
    Vector3 direction;
    Vector3 finaltarget;
    float currentVelocity;
    private RaycastHit hitinfo;
    private ParticleSystem marker;

    public AbilityWander(AbilityWanderData data, CharacterControl owner) : base(data, owner)
    {
        path=new NavMeshPath();
        if (owner.Profile == null)
        {
            return;
        }
        data.movePerSec=owner.Profile.movePerSec;
    }

    public override void Activate()
    {

    }

    public override void Deactivate()
    {

    }
    float elapese;
    public override void Update()
    {
        Debug.Log(1);
        elapese += Time.deltaTime;
        if (elapese > data.wanderStay)
        {
            RandomPosition();
            elapese = 0f;
        }
    }

    public override void FixedUpdate()
    {
        FollowPath();
    }

    void RandomPosition()
    {

        if (owner.isArrived == false)
        {
            return;
        }

        Vector3 rndpos = owner.transform.position + Random.insideUnitSphere * data.wanderRadius;
        rndpos.y=1f;
        Debug.Log(rndpos);

        SetDestination(rndpos);
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
        // else if (owner.isArrived == false && stopTrigger == true)
        // {
        //     stopTrigger = false;
        // }
        float a = owner.isArrived ? 0f : Mathf.Clamp01(currentVelocity);
        float movespd = Mathf.Lerp(owner.animator.GetFloat(AnimatorHashSet._MOVESPEED), a, Time.deltaTime * 10f);
        owner.animator?.SetFloat(AnimatorHashSet._MOVESPEED, movespd);
    }

    void SetDestination(Vector3 destination)
    {
        if (!NavMesh.CalculatePath(owner.transform.position, destination, NavMesh.AllAreas, path))
        {
            Debug.Log($"경로탐색 실패!{destination}");
            return;
        }
        corners = path.corners;
        next = 1;
        finaltarget = corners[corners.Length - 1];
        owner.isArrived = false;
    }

}
