using UnityEngine;
using CustomInspector;
using System.Collections;
using System.Linq;
using DG.Tweening;

public class CharacterEventControl : MonoBehaviour
{
    #region Event
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    [SerializeField] EventAttackDamage eventAttackDamage;
    [SerializeField] EventDeath eventDeath;
    [SerializeField] EventSensorSightEnter eventSensorSightEnter;
    [SerializeField] EventSensorSightExit eventSensorSightExit;
    [SerializeField] EventCursorHover eventCursorHover;
    [Space(10), HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l1;
    #endregion

    public CharacterControl cc;

    void Awake()
    {
        if (TryGetComponent(out cc) == false)
        {
            Debug.LogWarning($"GameEventControl ] CharacterControl 없음");
        }
    }

    void OnEnable()
    {
        eventPlayerSpawnAfter?.Register(OneventPlayerSpawnAfter);
        eventAttackDamage?.Register(OneventAttackDamage);
        eventDeath?.Register(OneventDeath);
        eventSensorSightEnter.Register(OneventSensorSightEnter);
        eventSensorSightExit.Register(OneventSensorSightExit);
        // eventCursorHover.Register(OneventCursorHover);

    }

    void OnDisable()
    {
        eventPlayerSpawnAfter?.Unregister(OneventPlayerSpawnAfter);
        eventAttackDamage?.Unregister(OneventAttackDamage);
        eventDeath?.Unregister(OneventDeath);
        eventSensorSightEnter?.Unregister(OneventSensorSightEnter);
        // eventCursorHover.Unregister(OneventCursorHover);

    }


    void OneventCameraSwitch(GameEventCameraSwitch e)
    {
        if (e.inout == true)
        {
            cc.abilityControl.Deactivate(AbilityFlag.MoveKeyboard);
            cc.abilityControl.Deactivate(AbilityFlag.MoveMouse);
        }
        else
        {
            cc.abilityControl.Activate(AbilityFlag.MoveKeyboard, false, null);
            cc.abilityControl.Activate(AbilityFlag.MoveMouse, false, null);
        }
    }

    void OneventPlayerSpawnAfter(EventPlayerSpawnAfter e)
    {
        StartCoroutine(SpawnSequence(e));
    }

    IEnumerator SpawnSequence(EventPlayerSpawnAfter e)
    {
        if (cc.Profile.models == null)
        {
            Debug.LogError("모델 없음");
        }
        var model = cc.Profile.models.Random();
        var clone = Instantiate(model, cc.model);


        var feedback = model.GetComponentInChildren<FeedbackControl>();
        if (feedback != null)
        {
            cc.feedbackControl = feedback;
        }
        clone.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().ForEach(m =>
        {
            m.gameObject.layer = LayerMask.NameToLayer("Silhouette");
        });


        if (cc.Profile.avatar == null)
        {
            Debug.LogError("아바타 없음");
        }

        cc.animator.avatar = cc.Profile.avatar;
        PoolManager.I.Spawn(e.particleSpawn, transform.position, Quaternion.identity, null);
        cc.Visible(true);
        cc.Animate(AnimatorHashSet.SPAWN, 0f);
        yield return new WaitForSeconds(1f);

        foreach (var v in cc.Profile.initialAbilities)
        {
            cc.abilityControl.AddAbility(v, true);
        }
        yield return new WaitForSeconds(1f);
        cc.ui.Show(true);

        //GameManager.I.DelayCallAsync(1000,()=>{Debug.Log(10);}).Forget();
    }
    #region damage
    void OneventSensorSightEnter(EventSensorSightEnter e)
    {
        if(cc!=e.from)
        {
            return;
        }
        
    }
    void OneventSensorSightExit(EventSensorSightExit e)
    {
        //바라보는 자신 체크, 바라볼 타겟 체크
        // if(cc!=e.from || cc.ik.target != e.to)
        // {
        //     return;
        // }
        // cc.ik.isTarget=false;
        // cc.ik.target=null;
    }
    void OneventAttackDamage(EventAttackDamage e)
    {
        if (cc != e.to)
        {
            return;
        }
        cc.abilityControl.Activate(AbilityFlag.Damage, false, e);
        //타격 이펙트
        PoolManager.I.Spawn(e.particleHit2, transform.position, Quaternion.identity, null);
    }
    #endregion

    #region Death
    void OneventDeath(EventDeath e)
    {
        if(cc!=e.target)
        {
            return;
        }
        cc.Animate(AnimatorHashSet.DEATH, 0.2f);
        cc.abilityControl.RemoveALL();
        // cc.Live(false);
        DOVirtual.DelayedCall(2f, ()=>cc.ui.Show(false));
        // cc.ik.isTarget=false;
    }
    #endregion
    // void OneventCursorHover(EventCursorHover e)
    // {
    //     cc.ik.isTarget=true;
    //     cc.ik.target=e.target.eyePoint;
    //     cc.LookatY(e.target.eyePoint.position);
    // }

    //비동기(Async)
    // 1. 코루틴 (Co-routine)
    // 2. Invoke
    // 3. async / await
    // 4. Awaitable
    // 5. CySharp-UniTask
}
