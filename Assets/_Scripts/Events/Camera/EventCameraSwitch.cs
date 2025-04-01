using UnityEngine;

[CreateAssetMenu]

public class GameEventCameraSwitch : GameEvent<GameEventCameraSwitch>
{
    public override GameEventCameraSwitch item=> this;

    public bool inout;
}
