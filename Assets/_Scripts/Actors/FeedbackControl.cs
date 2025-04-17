using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

public class FeedbackControl : MonoBehaviour
{
    [SerializeField] MMF_Player feedbackImpact;
    [SerializeField] MMF_Player feedbackSwingTrail;

    void Start()
    {
        PlaySwingTrail(false);
    }
    // 피격시 반짝임 효과
    public void PlayImpact()
    {
        feedbackImpact?.PlayFeedbacks();
    }

    public void PlaySwingTrail(bool on)
    {
        if(feedbackSwingTrail==null)
        {
            return;
        }
        var swing = feedbackSwingTrail.GetFeedbackOfType<MMF_VisualEffect>();

        if(swing == null)
        {
            return;
        }
        if(on)
        {
            swing.Mode= on ? MMF_VisualEffect.Modes.Play:  MMF_VisualEffect.Modes.Stop;
        }
        feedbackSwingTrail.PlayFeedbacks();
    }
}
