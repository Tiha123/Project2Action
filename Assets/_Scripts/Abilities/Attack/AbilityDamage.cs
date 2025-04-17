using UnityEngine;

public class AbilityDamage : Ability<AbilityDamageData>
{
    public AbilityDamage(AbilityDamageData data, CharacterControl ow) : base(data, ow)
    {
        owner.ui.SetHealth(owner.state.healthCurrent, owner.Profile.health);
        owner.ui.Show(true);
        owner.isDamageable=true;
    }

    public override void Activate(object obj = null)
    {
        EventAttackDamage e = obj as EventAttackDamage;
        if (e == null)
        {
            Debug.LogWarning("AbilityDamage ] EventAttackDamage 없음");
            return;
        }
        owner.isDamageable=true;

        owner.feedbackControl?.PlayImpact();
        Vector3 rndsphere = Random.insideUnitSphere;
        rndsphere.y = 0f;
        Vector3 rndpos = rndsphere * 0.25f + owner.eyePoint.position;
        var floating = PoolManager.I.Spawn(e.feeadbackFloatingText, rndpos, Quaternion.identity, owner.transform) as PoolableFeedbacks;
        if (floating != null)
        {
            floating.SetText($"{e.damage}");
        }
        owner.state.healthCurrent -= e.damage;
        owner.ui.SetHealth(owner.state.healthCurrent, owner.Profile.health);

        //tempcode
        if(owner.state.healthCurrent<=0)
        {
            data.eventDeath.target=owner;
            data.eventDeath.Raise();
        }
    }

    public override void Deactivate()
    {
        owner.isDamageable = false;
        owner.ui.Show(false);
    }
}