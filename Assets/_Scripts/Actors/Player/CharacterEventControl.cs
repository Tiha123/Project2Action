using UnityEngine;
using CustomInspector;
using System.Collections;
using System.Linq;

public class CharacterEventControl : MonoBehaviour
{
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventPlayerSpawnBefore eventPlayerSpawnBefore;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    [SerializeField] GameEventCameraSwitch eventCameraSwitch;

    [Space(10), HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l1;

    public CharacterControl cc;

    void Awake()
    {
        if (TryGetComponent(out cc)==false)
        {
            Debug.LogWarning($"GameEventControl ] CharacterControl 없음");
        }
    }

    void OnEnable()
    {
        eventPlayerSpawnAfter?.Register(OneventPlayerSpawnAfter);
        eventCameraSwitch?.Register(OneventCameraSwitch);
    }

    void OnDisable()
    {
        eventCameraSwitch?.Unregister(OneventCameraSwitch);
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
            cc.abilityControl.Activate(AbilityFlag.MoveKeyboard);
            cc.abilityControl.Activate(AbilityFlag.MoveMouse);
        }
    }

    void OneventPlayerSpawnAfter(EventPlayerSpawnAfter e)
    {
        StartCoroutine(SpawnSequence(e));
    }

    IEnumerator SpawnSequence(EventPlayerSpawnAfter e)
    {
        cc.profile=e.actorProfile;
        if (e.actorProfile.model == null)
        {
            Debug.LogError("모델 없음");
        }
        var clone = Instantiate(e.actorProfile.model, cc.model);
        clone.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().ForEach(m=>
        {
            m.gameObject.layer=LayerMask.NameToLayer("Silhouette");
        });


        if (e.actorProfile.avatar == null)
        {
            Debug.LogError("아바타 없음");
        }
        cc.animator.avatar = e.actorProfile.avatar;
        yield return new WaitForSeconds(1f);
        //GameManager.I.DelayCallAsync(1000,()=>{Debug.Log(10);}).Forget();
        PoolManager.I.Spawn(e.particleSpawn, transform.position, Quaternion.identity, null);


        cc.Visible(true);
        cc.Animate(cc._SPAWN, 0f);

    }




    //비동기(Async)
    // 1. 코루틴 (Co-routine)
    // 2. Invoke
    // 3. async / await
    // 4. Awaitable
    // 5. CySharp-UniTask
}
