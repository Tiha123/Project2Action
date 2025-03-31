using UnityEngine;

public class GameEventControl : MonoBehaviour
{
    [SerializeField] GameEventCameraSwitch eventCameraSwitch;
    CharacterControl cc;

    void Start()
    {
        if(TryGetComponent(out cc))
        {
            Debug.LogWarning($"GameEventControl ] CharacterControl 없음");
        }
    }

    void OnEnable()
    {
        eventCameraSwitch.Register(OneventCameraSwitch);
    }

    void OnDisable()
    {
        eventCameraSwitch.Unregister(OneventCameraSwitch);
    }

    void OneventCameraSwitch(GameEventCameraSwitch e)
    {
        if(e.inout==true)
        {
            cc.abilityControl.Deactivate(AbilityFlag.MoveKeyboard);
            cc.abilityControl.Deactivate(AbilityFlag.MoveMouse);
        }
        else
        {
            cc.abilityControl.Activate(AbilityFlag.MoveKeyboard);
            cc.abilityControl.Activate(AbilityFlag.MoveMouse);
        }
    }
}
