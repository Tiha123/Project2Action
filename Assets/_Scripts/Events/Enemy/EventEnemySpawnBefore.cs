using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventEnemySpawnBefore")]
public class EventEnemySpawnBefore : GameEvent<EventEnemySpawnBefore>
{
    public override EventEnemySpawnBefore item => this;

    public CharacterControl EnemyCC;

}
