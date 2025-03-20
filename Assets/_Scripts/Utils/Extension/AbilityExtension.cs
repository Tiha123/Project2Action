// 반복, 자주, 편의
using UnityEngine.Events;

public static class AbilityExtension
{
    public static bool Has(this ref AbilityFlag abilities, AbilityFlag a)
    {
        return (abilities&a)==a;
    }

    public static void Add(this ref AbilityFlag abilities, AbilityFlag a, UnityAction onComplete)
    {
        abilities |= a;
        onComplete?.Invoke();
    }

    public static void Remove(this ref AbilityFlag abilities, AbilityFlag a, UnityAction onComplete)
    {
        abilities &= ~a;
        onComplete?.Invoke();
    }

    public static void Use(this ref AbilityFlag abilities, AbilityFlag a, UnityAction action)
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
