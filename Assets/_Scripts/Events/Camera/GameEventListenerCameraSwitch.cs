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
        Debug.Log($"이벤트 {e} 인아웃");
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
