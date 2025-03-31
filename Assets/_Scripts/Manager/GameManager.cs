
using UnityEngine.Events;

//관리, 이벤트 송출
public class GameManager : BehaviourSingleton<GameManager>
{
    protected override bool isDontdestroy()=>true;

    public UnityAction eventCameraEvent;

    public void TriggerCameraEvent()=>eventCameraEvent?.Invoke();
}
