using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName ="GameEvent/EventCursorHover")]
public class EventCursorHover : GameEvent<EventCursorHover>
{
    public override EventCursorHover item => this;
}
