using Project2Action;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityWander : Ability<AbilityWanderData>
{
    public AbilityWander(AbilityWanderData data, IActorControl owner) : base(data, owner)
    {
        if(owner.Profile==null)
        {
            return;
        }
    }

    public override void Activate()
    {

    }

    public override void Deactivate()
    {


    }

    public override void FixedUpdate()
    {
        
    }

}
