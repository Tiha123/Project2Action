using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class PoolableFeedbacks : PoolBehaviour
{
    [SerializeField] MMF_Player pf;
    [SerializeField] TextMeshPro text;

    void Awake()
    {
        if (TryGetComponent(out pf) == false)
        {
            Debug.LogWarning("Poolable ] Feedbacks");
        }
    }

    
    public void SetText(string txt)
    {
        text.text=txt;
    }

    void OnEnable()
    {
        pf.RestoreInitialValues();
        pf.ResetFeedbacks();
        pf.PlayFeedbacks();
    }

    void OnDisable()
    {
        Despawn();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
