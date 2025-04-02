using UnityEngine;
using CustomInspector;
using System.Collections;

public class CharacterEventControl : MonoBehaviour
{
    [HorizontalLine("Events",color:FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventPlayerSpawnBefore eventPlayerSpawnBefore;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    [SerializeField] GameEventCameraSwitch eventCameraSwitch;

    [Space(10), HorizontalLine("Events",color:FixedColor.Blue), HideField] public bool _l1;

    CharacterControl cc;

    void Start()
    {
        if (TryGetComponent(out cc))
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
        yield return new WaitForSeconds(1f);
        //GameManager.I.DelayCallAsync(1000,()=>{Debug.Log(10);}).Forget();
        PoolManager.I.Spawn(e.particleSpawn,transform.position,Quaternion.identity, null);
        yield return new WaitForSeconds(0.2f);
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
