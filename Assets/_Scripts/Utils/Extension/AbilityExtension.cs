// 반복, 자주, 편의
using UnityEngine.Events;

public static class AbilityExtension
{
    public static bool Has(this ref Ability abilities, Ability a)
    {
        return (abilities&a)==a;
    }

    public static void Add(this ref Ability abilities, Ability a, UnityAction onComplete)
    {
        abilities |= a;
        onComplete?.Invoke();
    }

    public static void Remove(this ref Ability abilities, Ability a, UnityAction onComplete)
    {
        abilities &= ~a;
        onComplete?.Invoke();
    }

    public static void Use(this ref Ability abilities, Ability a, UnityAction action)
    {
        if(abilities.Has(a))
        {
            action?.Invoke();
        }
        else
        {

        }
    }
}
