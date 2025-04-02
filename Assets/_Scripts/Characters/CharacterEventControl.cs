using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using CustomInspector;

public class CharacterEventControl : MonoBehaviour
{
    [HorizontalLine("Events",color:FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventPlayerSpawnBefore eventPlayerSpawnBefore;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    [SerializeField] GameEventCameraSwitch eventCameraSwitch;

    [Space(10), HorizontalLine("Events",color:FixedColor.Blue), HideField] public bool _l1;

    CharacterControl cc;
    CancellationTokenSource disableCancellation = new CancellationTokenSource();
    CancellationTokenSource destroyCancellation = new CancellationTokenSource();
    CancellationTokenSource cts = new CancellationTokenSource();

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
        if (disableCancellation != null)
        {
            disableCancellation.Dispose();
        }
        disableCancellation = new CancellationTokenSource();
    }

    void OnDisable()
    {
        eventCameraSwitch?.Unregister(OneventCameraSwitch);
    }

    private void OnDestroy()
    {
        destroyCancellation.Cancel();
        destroyCancellation.Dispose();
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
        cc.Visible(true);
    }


    async UniTaskVoid DelayCall(int millisec, UnityAction oncomplete)
    {
        try
        {
            await UniTask.Delay(millisec, cancellationToken: cts.Token);
            oncomplete?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
        finally
        {
            cts.Cancel();
        }
    }

    //비동기(Async)
    // 1. 코루틴 (Co-routine)
    // 2. Invoke
    // 3. async / await
    // 4. Awaitable
    // 5. CySharp-UniTask
}
