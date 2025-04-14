using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventSensorAttackEnter")]
public class EventSensorAttackEnter : GameEvent<EventSensorAttackEnter>
{
    public override EventSensorAttackEnter item => this;

    [ReadOnly] public CharacterControl from;
    [ReadOnly] public CharacterControl to;
}
