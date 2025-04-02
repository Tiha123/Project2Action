using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventPlayerSpawnAfter")]
public class EventPlayerSpawnAfter : GameEvent<EventPlayerSpawnAfter>
{
    public override EventPlayerSpawnAfter item => this;

    [ReadOnly] public Transform eyePoint;
    [ReadOnly] public Transform cursorPoint;

    [Tooltip("플레이어 스폰 시 발동 파티클")]

    public PoolableParticle particleSpawn;
}
