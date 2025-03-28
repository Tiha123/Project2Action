
using UnityEngine.Events;

//관리, 이벤트 송출
public class GameEvent : BehaviourSingleton<GameEvent>
{
    protected override bool isDontdestroy()=>true;

    public UnityAction eventCameraEvent;

    public void TriggerCameraEvent()=>eventCameraEvent?.Invoke();
}
