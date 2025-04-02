using Unity.Cinemachine;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEventCameraSwitch eventCameraSwitch;
    [SerializeField] CinemachineVirtualCameraBase virtualCamera;

    void OnEnable()
    {
        eventCameraSwitch.Register(OnEventCameraSwitch);
    }

    void OnDisable()
    {
        eventCameraSwitch.Unregister(OnEventCameraSwitch);
    }

    void OnEventCameraSwitch(GameEventCameraSwitch e)
    {
        SwitchCamera(e.inout);
    }

    void SwitchCamera(bool on)
    {
        if (on)
        {
            virtualCamera.Priority+=1;
        }
        else
        {
            virtualCamera.Priority-=1;
        }
    }
}
