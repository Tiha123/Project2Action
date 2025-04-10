using UnityEngine;
using CustomInspector;
using System.Collections;
using System.Linq;

public class EnemyEventControl : MonoBehaviour
{
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventEnemySpawnAfter eventEnemySpawnAfter;
    [SerializeField] EventSensorTargetEnter eventSensorTargetEnter;
    [SerializeField] EventSensorTargetExit eventSensorTargetExit;

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
        eventSensorTargetEnter?.Register(OneventSensorTargetEnter);
        eventSensorTargetExit?.Register(OneventSensorTargetExit);
    }

    void OnDisable()
    {
        eventEnemySpawnAfter?.Unregister(OneventEnemySpawnAfter);
        eventSensorTargetEnter?.Unregister(OneventSensorTargetEnter);
        eventSensorTargetExit?.Unregister(OneventSensorTargetExit);
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

        cc.abilityControl.Activate(AbilityFlag.Wander);
    }
    #endregion



    void OneventSensorTargetEnter(EventSensorTargetEnter e)
    {
        if (e.from == cc)
        {
            cc.abilityControl.Activate(AbilityFlag.Trace, true);
        }

    }

    void OneventSensorTargetExit(EventSensorTargetExit e)
    {
        if (e.from == cc)
        {

            cc.abilityControl.Activate(AbilityFlag.Wander, true);
        }
    }



    //비동기(Async)
    // 1. 코루틴 (Co-routine)
    // 2. Invoke
    // 3. async / await
    // 4. Awaitable
    // 5. CySharp-UniTask
}
