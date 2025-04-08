using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AbilityWander : Ability<AbilityWanderData>
{
    EnemyControl ownerEC;

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

    public AbilityWander(AbilityWanderData data, IActorControl owner) : base(data, owner)
    {
        path=new NavMeshPath();
        ownerEC = (EnemyControl)owner;
        if (ownerEC.Profile == null)
        {
            return;
        }
        data.movePerSec=ownerEC.Profile.movePerSec;
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

        if (ownerEC.isArrived == false)
        {
            return;
        }

        Vector3 rndpos = ownerEC.transform.position + Random.insideUnitSphere * data.wanderRadius;
        rndpos.y=1f;
        Debug.Log(rndpos);

        SetDestination(rndpos);
    }


    void FollowPath()
    {
        if (corners == null || corners.Length <= 0 || ownerEC.isArrived == true)
        {
            return;
        }
        target = corners[next];
        direction = (target - ownerEC.transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            lookrot = Quaternion.LookRotation(direction);
        }

        ownerEC.transform.rotation = Quaternion.RotateTowards(ownerEC.transform.rotation, lookrot, data.rotatePerSec * Time.deltaTime);

        Vector3 movement = direction * data.movePerSec * 50f * Time.deltaTime;
        ownerEC.rb.linearVelocity = new Vector3(movement.x, ownerEC.rb.linearVelocity.y, movement.z);
        currentVelocity = Vector3.Distance(Vector3.zero, ownerEC.rb.linearVelocity);

        if (Vector3.Distance(target, ownerEC.transform.position) < data.stopDistance)
        {
            next++;
            if (next >= corners.Length)
            {
                ownerEC.isArrived = true;
                ownerEC.rb.linearVelocity = Vector3.zero;
            }
        }
    }

    void MoveAnimation()
    {
        // else if (owner.isArrived == false && stopTrigger == true)
        // {
        //     stopTrigger = false;
        // }
        float a = ownerEC.isArrived ? 0f : Mathf.Clamp01(currentVelocity);
        float movespd = Mathf.Lerp(ownerEC.animator.GetFloat(AnimatorHashSet._MOVESPEED), a, Time.deltaTime * 10f);
        ownerEC.animator?.SetFloat(AnimatorHashSet._MOVESPEED, movespd);
    }

    void SetDestination(Vector3 destination)
    {
        if (!NavMesh.CalculatePath(ownerEC.transform.position, destination, NavMesh.AllAreas, path))
        {
            Debug.Log($"경로탐색 실패!{destination}");
            return;
        }
        corners = path.corners;
        next = 1;
        finaltarget = corners[corners.Length - 1];
        ownerEC.isArrived = false;
    }

}
