
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

//관리, 이벤트 송출
public class GameManager : BehaviourSingleton<GameManager>
{
    protected override bool isDontdestroy()=>true;

    public UnityAction eventCameraEvent;

    CancellationTokenSource disableCancellation = new CancellationTokenSource();
    CancellationTokenSource destroyCancellation = new CancellationTokenSource();
    CancellationTokenSource cts = new CancellationTokenSource();

    void OnEnable()
    {
        if (disableCancellation != null)
        {
            disableCancellation.Dispose();
        }
        disableCancellation = new CancellationTokenSource();
    }

        private void OnDestroy()
    {
        destroyCancellation.Cancel();
        destroyCancellation.Dispose();
    }

    public void TriggerCameraEvent()=>eventCameraEvent?.Invoke();

    async public UniTaskVoid DelayCallAsync(int millisec, Action oncomplete)
    {
        try
        {
            await UniTask.Delay(millisec, cancellationToken: cts.Token);
            oncomplete?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            cts.Cancel();
        }
    }
}
