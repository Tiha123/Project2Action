using UnityEngine;
using CustomInspector;
using System.Collections;

public class AnimationEventListener : MonoBehaviour
{
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    [SerializeField] EventEnemySpawnAfter eventEnemySpawnAfter;
    [SerializeField] GameEventCameraSwitch eventCameraSwitch;
    [SerializeField] EventAttackBefore eventAttackBefore;

    [Space(10), HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l1;
    [ReadOnly] public CharacterControl owner;
    private Transform modelRoot;
    public PoolableParticle smoke, smoke2, swing1;

    [ReadOnly] public Transform footLeft;
    [ReadOnly] public Transform footRight;
    [ReadOnly] public Transform handLeft;
    [ReadOnly] public Transform handRight;

    void Awake()
    {
        if (TryGetComponent(out owner) == false)
        {
            Debug.LogWarning("AnimationEventListener ] CharacterControl 없음");
        }
        modelRoot = transform.FindSlot("_model_");
    }

    void OnEnable()
    {
        eventPlayerSpawnAfter.Register(OneventPlayerSpawnAfter);
        eventEnemySpawnAfter.Register(OneventEnemySpawnAfter);
    }

    void OnDisable()
    {
        
    }

    public void OneventPlayerSpawnAfter(EventPlayerSpawnAfter e)
    {
        if(owner == e.cc)
        {
            StartCoroutine(FindSlots());
        }
    }

    public void OneventEnemySpawnAfter(EventEnemySpawnAfter e)
    {
        if(owner == e.cc)
        {
            StartCoroutine(FindSlots());
        }
    }


    IEnumerator FindSlots()
    {
        yield return new WaitUntil(() => owner.animator.avatar != null);
        if (modelRoot == null)
        {
            Debug.LogWarning("AnimationEventListener ] Modelroot 없음");
        }
        footLeft = modelRoot.FindSlot("leftfoot", "lfoot", "l foot");

        footRight = modelRoot.FindSlot("rightfoot", "rfoot", "r foot");

        handLeft=modelRoot.FindSlot("L Hand", "lefthand");

        handRight=modelRoot.FindSlot("R Hand", "RightHand");
    }



    public void Footstep(string s)
    {
        if (owner.isArrived == true)
        {
            return;
        }

        if (s == "L")
        {
            PoolManager.I.Spawn(smoke, footLeft.position, Quaternion.identity, null);
        }

        if (s == "R")
        {
            PoolManager.I.Spawn(smoke, footRight.position, Quaternion.identity, null);
        }
    }

    public void JumpDown()
    {
        Vector3 offset = owner.model.position + Vector3.up * 0.1f;
        PoolManager.I.Spawn(smoke2, offset, Quaternion.identity, null);
    }

    public void Attack(string s)
    {
        eventAttackBefore.Raise();
        if(Random.Range(0,10)>=7)
        {
            var rot = Quaternion.LookRotation(owner.transform.forward, Vector3.up);
            rot.eulerAngles=new Vector3(-90f,rot.eulerAngles.y,0f);
            PoolManager.I.Spawn(swing1, s=="L" ? handLeft.position : handRight.position, rot, null);
        }
        else
        {
            return;
        }
    }

    public void Swing(string on)
    {
        owner.feedbackControl.PlaySwingTrail(on.ToLower() == "on");
    }
}
