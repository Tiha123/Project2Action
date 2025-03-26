using UnityEngine;

// template: 사용법 <T>
// 싱글톤: 관리자, 전역, 하나
// BS: 런타임 시에만 존재, 에디터에서도 존재하게 하려면?
public abstract class BehaviourSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T _instance;

    public static T I
    {
        get 
        {
            if(_instance==null)
            {
               _instance=FindFirstObjectByType<T>(FindObjectsInactive.Include);
               if(_instance==null)
               {
                    GameObject o=new GameObject(typeof(T).Name);
                    _instance=o.AddComponent<T>();
               }
            }
            return _instance;
        }
    }

    protected abstract bool isDontdestroy();

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        if(I!=null&&I!=this)
        {
            Destroy(gameObject);
            return;
        }
        if(isDontdestroy())
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
