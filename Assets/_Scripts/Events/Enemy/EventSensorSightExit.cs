using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/EventSensorSightExit")]
public class EventSensorSightExit : GameEvent<EventSensorSightExit>
{
    public override EventSensorSightExit item => this;

    public CharacterControl from;
    public CharacterControl to;
}
