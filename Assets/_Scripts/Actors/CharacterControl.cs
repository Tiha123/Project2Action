using UnityEngine;
using CustomInspector;
using ParadoxNotion.Animation;
using DG.Tweening;
//임시
public struct CharacterState
{
    public int healthCurrent;

    public int attackDamage;
    public void Set(ActorProfile profile)
    {
        healthCurrent=profile.health;
    }
}
//임시
// GAS(Game ability system)

// 캐릭터관리
public class CharacterControl : MonoBehaviour
{


    [HideInInspector] public AbilityControl abilityControl;
    [HideInInspector] public UIControl ui;
    [HideInInspector] public FeedbackControl feedbackControl;
    [HideInInspector] public AnimationIKControl ik;

    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isArrived = true;
    [ReadOnly] public bool isDamageable;
    public Transform eyePoint;
    [ReadOnly] public Transform model;

    public float isGroundedOffset = 1.1f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;
    [SerializeField, ReadOnly] private ActorProfile profile;
    public CharacterState state;
    Tween tweenrot;

    
    public ActorProfile Profile 
    { 
        get => profile; 
        set => profile=value; 
    }

    void Awake()
    {
        if (TryGetComponent(out abilityControl) == false)
        {
            Debug.LogWarning("CharacterControl ] AbilityControl없음");
        }
        if (TryGetComponent(out rb) == false)
        {
            Debug.LogWarning("CharacterControl ] Rigidbody없음");
        }
        if (TryGetComponent(out animator) == false)
        {
            Debug.LogWarning("CharacterControl ] Animator없음");
        }
        if (TryGetComponent(out ui) == false)
        {
            Debug.LogWarning("CharacterControl ] UIControl없음");
        }
        ik=GetComponent<AnimationIKControl>();
        
        model=transform.Find("_Model_");
        eyePoint=transform.Find("_Eyepoint_");
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, isGroundedOffset);
    }

    

    public void Visible(bool b)
    {
        model.gameObject.SetActive(b);
    }
    
    public void Animate(int hash, float duration, int layer = 0)
    {
        animator?.CrossFadeInFixedTime(hash,duration,layer,0f);
    }

    public void Animate(string name, AnimatorOverrideController aoc, AnimationClip clip, float anispd, float duration, int layer = 0)
    {
        if(animator==null)
        {
            return;
        }
        animator.runtimeAnimatorController=aoc;
        aoc[name]=clip;
        animator?.CrossFadeInFixedTime(name,duration,layer,0f);
    }

    // 타겟을 바라본다 (y축만 회전)
    Quaternion targetrotation;
    public void LookatY(Vector3 target)
    {
        if(tweenrot!=null)
        {
            tweenrot.Kill(true);
        }
        target.y=0;
        Vector3 direction = target - new Vector3(eyePoint.position.x, 0f, eyePoint.position.z);
        targetrotation = Quaternion.LookRotation(direction.normalized);
        tweenrot= transform.DORotateQuaternion(targetrotation,0.2f).SetEase(Ease.OutSine);
        // transform.rotation = Quaternion.Euler(rot);
    }

    #region ANIMATE
    
    public void AnimateMoveSpeed(float targetspeed, bool immediate)
    {
        if(animator==null)
        {
            return;
        }
        if(immediate==true)
        {
            animator.SetFloat(AnimatorHashSet.MOVESPEED, targetspeed);
        }
        else
        {
            float curr = animator.GetFloat(AnimatorHashSet.MOVESPEED);
            float spd = Mathf.Lerp(curr, targetspeed, Time.deltaTime * 10f);
            animator.SetFloat(AnimatorHashSet.MOVESPEED, spd);
        }

    }

    public void AnimateTrigger(string key, AnimatorOverrideController aoc, AnimationClip clip)
    {
        if(animator=null)
        {
            return;
        }
        aoc[key]=clip;
        animator.runtimeAnimatorController=aoc;
        animator.SetTrigger(key);
    }

    #endregion
}
