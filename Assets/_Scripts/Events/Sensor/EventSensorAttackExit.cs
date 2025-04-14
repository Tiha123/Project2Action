using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/EventSensorAttackExit")]
public class EventSensorAttackExit : GameEvent<EventSensorAttackExit>
{
    public override EventSensorAttackExit item => this;

    public CharacterControl from;
    public CharacterControl to;
}
