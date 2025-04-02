using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventPlayerSpawnAfter")]
public class EventPlayerSpawnAfter : GameEvent<EventPlayerSpawnAfter>
{
    public override EventPlayerSpawnAfter item => this;

    [ReadOnly] public Transform eyePoint;
    [ReadOnly] public Transform cursorPoint;
}
