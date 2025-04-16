using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;

//관리, 이벤트 송출
public class GameManager : BehaviourSingleton<GameManager>
{
    [SerializeField] MMF_Player feedbackInformation;
    [SerializeField] TextMeshProUGUI textInformation;
    protected override bool isDontdestroy()=>true;

    public void ShowInfo(string info, float duration =1f)
    {
        if(feedbackInformation.IsPlaying==true)
        {
            feedbackInformation.StopFeedbacks();
        }
        textInformation.text=info;
        feedbackInformation.GetFeedbackOfType<MMF_Pause>().PauseDuration=duration;
        feedbackInformation.PlayFeedbacks();
    }

    // public UnityAction eventCameraEvent;

    // CancellationTokenSource disableCancellation = new CancellationTokenSource();
    // CancellationTokenSource destroyCancellation = new CancellationTokenSource();
    // CancellationTokenSource cts = new CancellationTokenSource();

    // void OnEnable()
    // {
    //     if (disableCancellation != null)
    //     {
    //         disableCancellation.Dispose();
    //     }
    //     disableCancellation = new CancellationTokenSource();
    // }

    //     private void OnDestroy()
    // {
    //     destroyCancellation.Cancel();
    //     destroyCancellation.Dispose();
    // }

    // public void TriggerCameraEvent()=>eventCameraEvent?.Invoke();

    // async public UniTaskVoid DelayCallAsync(int millisec, Action oncomplete)
    // {
    //     try
    //     {
    //         await UniTask.Delay(millisec, cancellationToken: cts.Token);
    //         oncomplete?.Invoke();
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e);
    //     }
    //     finally
    //     {
    //         cts.Cancel();
    //     }
    // }
}
