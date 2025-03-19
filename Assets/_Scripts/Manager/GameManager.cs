
using UnityEngine.Events;

//관리, 이벤트 송출
public class GameManager : BehaviourSingleton<GameManager>
{
    protected override bool isDontdestroy()=>true;

//Events
    public UnityAction<Ability> eventAbilityAdded;
    public UnityAction<Ability> eventAbilityRemoveded;
    public UnityAction<Ability> eventAbilityUsed;

//Trigger
    public void TriggerAbilityAdd(Ability a)
    {
        eventAbilityAdded?.Invoke(a);
    }
}
