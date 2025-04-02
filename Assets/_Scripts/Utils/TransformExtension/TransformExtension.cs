using UnityEngine;

public static class TransformExtension
{
    public static Transform FindSlot(this Transform t, string slotname)
    {
        Transform[] children= t.GetComponentsInChildren<Transform>();
        foreach(Transform v in children)
        {
            if(v.name.ToLower().Contains(slotname))
            {
                return v;
            }
        }
        Debug.LogWarning($"탐색 실패 {slotname}");
        return null;
    }
}
