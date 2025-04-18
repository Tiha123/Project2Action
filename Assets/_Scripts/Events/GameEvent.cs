using UnityEngine;
using UnityEngine.Events;

public abstract class GameEvent<T> : ScriptableObject where T : GameEvent<T>
{
    abstract public T item { get ;}

    public UnityAction<T> OnEventRaised;

    public void Raise()
    {
        OnEventRaised?.Invoke(item);
    }

    public void Register(UnityAction<T> listener)
    {
        OnEventRaised += listener;
    }
    public void Unregister(UnityAction<T> listener)
    {
        OnEventRaised -= listener;
    }
    public void Clear(UnityAction<T> listener)
    {
        OnEventRaised=null;
    }
}
