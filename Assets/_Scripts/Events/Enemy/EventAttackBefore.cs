using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/EventAttackBefore")]
public class EventAttackBefore : GameEvent<EventAttackBefore>
{
    public override EventAttackBefore item => this;

    public CharacterControl from;
}
