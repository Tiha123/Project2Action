using MoreMountains.Feedbacks;
using UnityEngine;

public class FeedbackControl : MonoBehaviour
{
    [SerializeField] MMF_Player feedbackImpact;
    // 피격시 반짝임 효과
    public void PlayImpact()
    {
        feedbackImpact?.PlayFeedbacks();
    }
}
