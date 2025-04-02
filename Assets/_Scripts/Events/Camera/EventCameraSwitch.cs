
using UnityEngine;
using CustomInspector;

[CreateAssetMenu(menuName ="GameEvent/EventCameraSwitch")]

public class GameEventCameraSwitch : GameEvent<GameEventCameraSwitch>
{
    public override GameEventCameraSwitch item=> this;

    [ReadOnly] public bool inout;
}
