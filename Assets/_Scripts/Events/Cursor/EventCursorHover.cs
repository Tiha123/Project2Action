using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName ="GameEvent/EventCursorHover")]
public class EventCursorHover : GameEvent<EventCursorHover>
{
    public override EventCursorHover item => this;

    [ReadOnly] public CharacterControl target;
}
