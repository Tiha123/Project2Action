using UnityEngine;

public class AnimationIKControl : MonoBehaviour
{
    private Animator animator;
    public Transform target;
    [Range(0f,1f), SerializeField] float overallWeight=1f;
    [Range(0f,1f), SerializeField] float headWeight=0.5f;
    [Range(0f,1f), SerializeField] float bodyWeight=0.5f;
    [Range(0f,1f), SerializeField] float currentWeight=0f;
    public bool isTarget;
    
    void Start()
    {
        if(TryGetComponent(out animator)==false)
        {
            return;
        }
    }

    void Update()
    {
        if(animator==null||target==null)
        {
            return;
        }
        float targetWeight=(isTarget&&target!=null) ? overallWeight : 0f;
        currentWeight = Mathf.MoveTowards(currentWeight, targetWeight, Time.deltaTime*10f);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if(target==null||currentWeight<=0.01f)
        {
            animator.SetLookAtWeight(0f);
            return;
        }

        animator.SetLookAtWeight(currentWeight, bodyWeight, headWeight);
        animator.SetLookAtPosition(target.position);
    }
}
