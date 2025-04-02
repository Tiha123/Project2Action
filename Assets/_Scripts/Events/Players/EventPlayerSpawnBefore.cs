using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventPlayerSpawnBefore")]
public class EventPlayerSpawnBefore : GameEvent<EventPlayerSpawnBefore>
{
    public override EventPlayerSpawnBefore item => this;

    [ReadOnly] public CharacterControl playerCC;
    [ReadOnly] public CursorControl playerCursor;
    [ReadOnly] public CameraControl playerCamera;
}
