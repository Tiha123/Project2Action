using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventAttackAfter")]
public class EventAttackDamage : GameEvent<EventAttackDamage>
{
    public override EventAttackDamage item => this;

    [ReadOnly] public CharacterControl from;
    [ReadOnly] public CharacterControl to;
    [ReadOnly] public int damage;
    [Tooltip("공격 파티클")] public PoolableParticle particleHit2;
    public PoolableFeedbacks feeadbackFloatingText;
}
