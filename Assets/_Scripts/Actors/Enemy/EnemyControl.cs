using UnityEngine;
using CustomInspector;
using Unity.Cinemachine;

// GAS(Game ability system)

// 캐릭터관리
public class EnemyControl : MonoBehaviour, IActorControl
{

    [HideInInspector] public AbilityControl abilityControl;

    [ReadOnly] public bool isArrived = true;
    [ReadOnly] public Transform eyePoint;
    [ReadOnly] public Transform model;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;
    [SerializeField, ReadOnly] private ActorProfile profile;

    public CinemachineVirtualCameraBase maincamera;

    public ActorProfile Profile 
    { 
        get => profile; 
        set => profile=value; 
    }

    void Awake()
    {
        if (TryGetComponent(out abilityControl) == false)
        {
            Debug.LogWarning("EnemyControl ] AbilityControl없음");
        }
        if (TryGetComponent(out rb) == false)
        {
            Debug.LogWarning("EnemyControl ] Rigidbody없음");
        }
        if (TryGetComponent(out animator) == false)
        {
            Debug.LogWarning("EnemyControl ] Animator없음");
        }
        model = transform.Find("_Model_");
        eyePoint = transform.Find("_Eyepoint_");
    }

    public void Visible(bool b)
    {
        model.gameObject.SetActive(b);
    }

    public void Animate(int hash, float duration, int layer = 0)
    {
        animator?.CrossFadeInFixedTime(hash, duration, layer, 0f);
    }
}
