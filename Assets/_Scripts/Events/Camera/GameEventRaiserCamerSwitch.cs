using UnityEngine;
using Unity.Cinemachine;
using System.Threading.Tasks;

[CreateAssetMenu]
public class GameEventSenderCameraSwitch : MonoBehaviour
{
    [SerializeField] GameEventCameraSwitch eventcameraSwitch;

    [SerializeField] CinemachineVirtualCameraBase virtualCamera;

    void Start()
    {
        virtualCamera.Priority=0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag!="Player")
        {
            return;
        }
        SwitchCameraAsync(2000);
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag!="Player")
        {
            return;
        }
        eventcameraSwitch.inout=false;
        eventcameraSwitch?.Raise();
    }
    async void SwitchCameraAsync(int t=1000)
    {
        try
        {
            eventcameraSwitch.inout=true;
            eventcameraSwitch?.Raise();
            await Task.Delay(t);

            eventcameraSwitch.inout=false;
            eventcameraSwitch?.Raise();
        }
        catch(System.Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {

        }
    }
}