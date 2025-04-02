using UnityEngine;
using CustomInspector;

public class AnimationEventListener : MonoBehaviour
{
    [ReadOnly] public CharacterControl cc;
    private Transform modelRoot;
    public PoolableParticle smoke, smoke2;

    [ReadOnly] public Transform footLeft;
    [ReadOnly] public Transform footRight;

    void Awake()
    {
        if(TryGetComponent(out cc)==false)
        {   
            Debug.LogWarning("AnimationEventListener ] CharacterControl 없음");
        }
    }

    void OnValidate()
    {
        modelRoot=transform.FindSlot("_model_");
        if(modelRoot==null)
        {
            Debug.LogWarning("AnimationEventListener ] Modelroot 없음");
        }
        footLeft=modelRoot.FindSlot("leftfoot");
        footRight=modelRoot.FindSlot("rightfoot");
    }

    

    public void Footstep(string s)
    {
        if (cc.isArrived==true)
        {   
            return;
        }

        if(s=="L")
        {
            PoolManager.I.Spawn(smoke, footLeft.position, Quaternion.identity, null);
        }

        if(s=="R")
        {
            PoolManager.I.Spawn(smoke, footRight.position, Quaternion.identity, null);
        }
    }

    public void JumpDown()
    {
        Vector3 offset=cc.model.position+Vector3.up*0.1f;
        PoolManager.I.Spawn(smoke2, offset, Quaternion.identity, null);
    }
}
