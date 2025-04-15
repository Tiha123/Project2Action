using UnityEngine;
using CustomInspector;
using System.Collections;
using System.Linq;

public class CharacterEventControl : MonoBehaviour
{
    #region Event
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    [SerializeField] EventAttackDamage eventAttackDamage;
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
    }

    void OnDisable()
    {
        eventPlayerSpawnAfter?.Unregister(OneventPlayerSpawnAfter);
        eventAttackDamage?.Unregister(OneventAttackDamage);
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
        cc.Animate(AnimatorHashSet._SPAWN, 0f);
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
    void OneventAttackDamage(EventAttackDamage e)
    {
        if (cc != e.to)
        {
            return;
        }
        //타격 이펙트
        PoolManager.I.Spawn(e.particleHit2, transform.position, Quaternion.identity, null);
        Vector3 rndsphere = Random.insideUnitSphere;
        rndsphere.y = 0f;
        Vector3 rndpos = rndsphere * 0.25f + cc.eyePoint.position;
        var floating = PoolManager.I.Spawn(e.feeadbackFloatingText, rndpos, Quaternion.identity, cc.transform) as PoolableFeedbacks;
        if(floating!=null)
        {
            floating.SetText($"{e.damage}");
        }
        cc.state.healthCurrent -= e.damage;
        cc.ui.SetHealth(cc.state.healthCurrent, cc.Profile.health);
    }
    #endregion

    //비동기(Async)
    // 1. 코루틴 (Co-routine)
    // 2. Invoke
    // 3. async / await
    // 4. Awaitable
    // 5. CySharp-UniTask
}
