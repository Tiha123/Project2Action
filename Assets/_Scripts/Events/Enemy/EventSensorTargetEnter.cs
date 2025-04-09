using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventSensorTargetEnter")]
public class EventSensorTargetEnter : GameEvent<EventSensorTargetEnter>
{
    public override EventSensorTargetEnter item => this;

    public CharacterControl from;
    public CharacterControl to;
}
