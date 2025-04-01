using UnityEngine;

[CreateAssetMenu(fileName = "EventPlayerSpawnAfter", menuName = "Scriptable Objects/EventPlayerSpawnAfter")]
public class EventPlayerSpawnAfter : GameEvent<EventPlayerSpawnAfter>
{
    public override EventPlayerSpawnAfter item => this;

    public Transform eyePoint;
    public Transform cursorPoint;
}
