using UnityEngine;
using CustomInspector;
using TMPro;
using UnityEngine.Rendering;

// GAS(Game ability system)

// 캐릭터관리
public class CharacterControl : MonoBehaviour
{


    [HideInInspector] public AbilityControl abilityControl;

    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isArrived = true;
    public Transform eyePoint;
    [ReadOnly] public Transform model;

    public float isGroundedOffset = 1.1f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;
    [SerializeField, ReadOnly] private ActorProfile profile;
    [ReadOnly] public TextMeshPro uiinfo;

    
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
        model=transform.Find("_Model_");
        eyePoint=transform.Find("_Eyepoint_");

        uiinfo=GetComponentInChildren<TextMeshPro>();
        if(uiinfo==null)
        {
            Debug.Log("CharacterControl ] _INFO_ 없음");
        }
        
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

    public void Display(string info)
    {
        if (uiinfo==null)
        {
            return;
        }
        
        uiinfo.text=info;
    }
}
