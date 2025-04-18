using UnityEngine;
using CustomInspector;
using System.Collections;
using System.Linq;

public class EnemyEventControl : MonoBehaviour
{
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventEnemySpawnAfter eventEnemySpawnAfter;
    [SerializeField] EventSensorSightEnter eventSensorSightEnter;
    [SerializeField] EventSensorSightExit eventSensorSightExit;
    [SerializeField] EventSensorAttackEnter eventSensorAttackEnter;
    [SerializeField] EventSensorAttackExit eventSensorAttackExit;
    [SerializeField] EventAttackDamage eventAttackDamage;
    [SerializeField] EventDeath eventDeath;


    [Space(10), HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l1;

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
        eventEnemySpawnAfter?.Register(OneventEnemySpawnAfter);
        eventSensorSightEnter?.Register(OneventSensorSightEnter);
        eventSensorSightExit?.Register(OneventSensorSightExit);
        eventSensorAttackEnter?.Register(OneventSensorAttackEnter);
        eventSensorAttackExit?.Register(OneventSensorAttackExit);
        eventAttackDamage?.Register(OneventAttackDamage);
    }

    void OnDisable()
    {
        eventEnemySpawnAfter?.Unregister(OneventEnemySpawnAfter);
        eventSensorSightEnter?.Unregister(OneventSensorSightEnter);
        eventSensorSightExit?.Unregister(OneventSensorSightExit);
        eventSensorAttackEnter?.Unregister(OneventSensorAttackEnter);
        eventSensorAttackExit?.Unregister(OneventSensorAttackExit);
        eventAttackDamage?.Unregister(OneventAttackDamage);
    }



    #region Event-SpawnAfter
    void OneventEnemySpawnAfter(EventEnemySpawnAfter e)
    {
        if (cc != e.cc)
        {
            return;
        }
        StartCoroutine(SpawnSequence(e));
    }

    IEnumerator SpawnSequence(EventEnemySpawnAfter e)
    {
        if (cc.Profile.models == null)
        {
            Debug.LogError("모델 없음");
        }
        var model=cc.Profile.models.Random();
        var clone = Instantiate(model, cc.model);
        clone.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().ForEach(m =>
        {
            m.gameObject.layer = LayerMask.NameToLayer("Silhouette");
        });


        if (cc.Profile.avatar == null)
        {
            Debug.LogError("아바타 없음");
        }
        cc.animator.avatar = cc.Profile.avatar;
        yield return new WaitForSeconds(1f);
        cc.Visible(true);

        foreach (var dat in cc.Profile.initialAbilities)
        {
            cc.abilityControl.AddAbility(dat, false);
        }

        yield return new WaitForEndOfFrame();

        if (TryGetComponent<CursorSelectable>(out CursorSelectable sel) == true)
        {
            sel.SetupRenderer();
        }

        cc.abilityControl.Activate(AbilityFlag.Wander, true, null);
    }
    #endregion



    void OneventSensorSightEnter(EventSensorSightEnter e)
    {
        if (e.from == cc)
        {
            cc.abilityControl.Activate(AbilityFlag.Trace, true, e.to);
        }

    }

    void OneventSensorSightExit(EventSensorSightExit e)
    {
        if (e.from == cc)
        {
            cc.abilityControl.Activate(AbilityFlag.Wander, true, null);
        }
    }

    void OneventSensorAttackEnter(EventSensorAttackEnter e)
    {
        if (e.from == cc)
        {
            cc.abilityControl.Activate(AbilityFlag.Attack, true, e.to);
        }
    }

    void OneventSensorAttackExit(EventSensorAttackExit e)
    {
        if (e.from == cc)
        {
            cc.abilityControl.Activate(AbilityFlag.Trace, true, e.to);
        }
    }

    void OneventAttackDamage(EventAttackDamage e)
    {
        if (cc != e.to || cc==e.from)
        {
            return;
        }
        if(e.from.Profile.actorType != ActorType.Player)
        {
            return;
        }
        cc.abilityControl.Activate(AbilityFlag.Damage, false, e);
        //타격 이펙트
        PoolManager.I.Spawn(e.particleHit2, transform.position, Quaternion.identity, null);
    }

    void OneventDeath(EventDeath e)
    {
        if(cc!=e.target)
        {
            return;
        }
        AnimationClip clip=cc.Profile.DAMAGE.Random();

        cc.AnimateTrigger("ATTACK", cc.Profile.aoc, clip);
    }



    //비동기(Async)
    // 1. 코루틴 (Co-routine)
    // 2. Invoke
    // 3. async / await
    // 4. Awaitable
    // 5. CySharp-UniTask
}
