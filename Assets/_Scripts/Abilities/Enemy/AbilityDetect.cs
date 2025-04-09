
using UnityEngine;

public class AbilityDetect : Ability<AbilityDetectData>
{
    public AbilityDetect(AbilityDetectData data, CharacterControl ow) : base(data, ow)
    {

    }

    public override void Activate()
    {
        GameObject p=GameObject.FindGameObjectWithTag("Player");
    }

    public override void Deactivate()
    {

    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }
}
