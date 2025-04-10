using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/EventSensorAttackEnter")]
public class EventSensorAttackEnter : GameEvent<EventSensorAttackEnter>
{
    public override EventSensorAttackEnter item => this;

    public CharacterControl from;
    public CharacterControl to;
}
