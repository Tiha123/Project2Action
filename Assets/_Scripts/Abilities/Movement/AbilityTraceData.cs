using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "Abilities/Trace")]
public class AbilityTraceData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Trace;

    [ReadOnly] public float movePerSec = 10f;
    [ReadOnly] public float rotatePerSec = 1080f;
    public float stopDistance = 0.1f;

    public override Ability CreateAbility(CharacterControl owner) => new AbilityTrace(this, owner);

    [Tooltip("추격 대상")]
    public CharacterControl traceTarget;

    public EventSensorAttackEnter eventSensorAttackEnter;

    public EventSensorAttackExit eventSensorAttackExit;

}
