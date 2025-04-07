using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventEnemySpawnAfter")]
public class EventEnemySpawnAfter : GameEvent<EventEnemySpawnAfter>
{
    public override EventEnemySpawnAfter item => this;

    [ReadOnly] public Transform eyePoint;
    [ReadOnly] public ActorProfile actorProfile;

    [Tooltip("플레이어 스폰 시 발동 파티클")]
    public PoolableParticle particleSpawn;
}
