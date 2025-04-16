using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventsDeath")]
public class EventDeath : GameEvent<EventDeath>
{
    public override EventDeath item => this;

    [ReadOnly] public CharacterControl playerCC;
    
    [ReadOnly] public CharacterControl target;
}
