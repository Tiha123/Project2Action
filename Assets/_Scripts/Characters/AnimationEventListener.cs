using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    private CharacterControl cc;
    public Transform Lfoot, Rfoot, Root;
    public PoolableParticle smoke, smoke2;

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

    public void JumpDown()
    {
        Vector3 offset=Root.position+Vector3.up*0.1f;
        PoolManager.I.Spawn(smoke2, offset, Quaternion.identity, null);
    }
}
