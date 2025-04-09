using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventSensorTargetExit")]
public class EventSensorTargetExit : GameEvent<EventSensorTargetExit>
{
    public override EventSensorTargetExit item => this;

    public CharacterControl from;
    public CharacterControl to;
}
