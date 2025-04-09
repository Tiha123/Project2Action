using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName = "GameEvent/EventEnemyDetect")]
public class EventEnemyDetect : GameEvent<EventEnemyDetect>
{
    public override EventEnemyDetect item => this;

    public bool isDetected=false;
}
