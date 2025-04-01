using UnityEngine;

[CreateAssetMenu(fileName = "EventPlayerSpawnBefore", menuName = "Scriptable Objects/EventPlayerSpawnBefore")]
public class EventPlayerSpawnBefore : GameEvent<EventPlayerSpawnBefore>
{
    public override EventPlayerSpawnBefore item => this;

    public CharacterControl playerCC;
    public CursorControl playerCursor;
    public CameraControl playerCamera;
}
