
using System.Collections.Generic;

public static class ListExtension
{
    public static T Random<T>(this List<T> list)
    {
        if(list==null || list.Count <=0)
        {
            return default;
        }
        int rnd=UnityEngine.Random.Range(0,list.Count);

        return list[rnd];
    }
}
