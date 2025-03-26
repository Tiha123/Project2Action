using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    private CharacterControl cc;
    public Transform Lfoot, Rfoot;
    public PoolableParticle smoke;

    void Awake()
    {
        if(TryGetComponent(out cc)==false)
        {   
            Debug.LogWarning("AnimationEventListener ] CharacterControl 없음");
        }
    }

    public void Footstep(string s)
    {
        if (cc.isArrived==true)
        {   
            return;
        }

        if(s=="L")
        {
            PoolManager.I.Spawn(smoke, Lfoot.position, Quaternion.identity, null);
        }

        if(s=="R")
        {
            PoolManager.I.Spawn(smoke, Rfoot.position, Quaternion.identity, null);
        }
    }
}
