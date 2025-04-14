using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/EventSensorSightEnter")]
public class EventSensorSightEnter : GameEvent<EventSensorSightEnter>
{
    public override EventSensorSightEnter item => this;

    public CharacterControl from;
    public CharacterControl to;
}
