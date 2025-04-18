using UnityEngine;
using UnityEngine.AI;

public class AbilityWander : Ability<AbilityWanderData>
{
    private NavMeshPath path;
    private Vector3[] corners;
    int next;
    Quaternion lookrot;
    Vector3 target;
    Vector3 direction;
    float currentVelocity;

    public AbilityWander(AbilityWanderData data, CharacterControl owner) : base(data, owner)
    {
        path=new NavMeshPath();
        if (owner.Profile == null)
        {
            return;
        }
        data.movePerSec=owner.Profile.movePerSec;
    }

    public override void Activate(object obj = null)
    {
        
    }

    public override void Deactivate()
    {
        owner.isArrived=true;
    }
    float elapsed;
    public override void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > data.wanderStay)
        {
            RandomPosition();
            elapsed = 0f;
        }
        MoveAnimation();
    }

    public override void FixedUpdate()
    {
        if (owner == null || owner.rb == null) return;
        FollowPath();
    }

    void RandomPosition()
    {

        if (owner.isArrived == false)
        {
            return;
        }

        Vector3 rndpos = owner.transform.position + Random.insideUnitSphere * data.wanderRadius;
        rndpos.y=0.9f;

        SetDestination(rndpos);
    }


    void FollowPath()
    {
        owner.ui.Display(data.Flag.ToString());
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
        float a = owner.isArrived ? 0f : Mathf.Clamp01(currentVelocity/data.movePerSec);
        float movespd = Mathf.Lerp(owner.animator.GetFloat(AnimatorHashSet.MOVESPEED), a, Time.deltaTime * 10f);
        owner.animator?.SetFloat(AnimatorHashSet.MOVESPEED, movespd);
    }

    void SetDestination(Vector3 destination)
    {
        if (!NavMesh.CalculatePath(owner.transform.position, destination, NavMesh.AllAreas, path))
        {
            return;
        }
        corners = path.corners;
        next = 1;
        owner.isArrived = false;
    }
    
    

}
