using System.Collections.Generic;
using UnityEngine;
using CustomInspector;
using Unity.Cinemachine;
using Project2Action;

// GAS(Game ability system)

// 캐릭터관리
public class CharacterControl : MonoBehaviour
{

    #region Animator Hashset
    [HideInInspector] public int _MOVESPEED = Animator.StringToHash("movespeed");
    [HideInInspector] public int _RUNTOSTOP = Animator.StringToHash("RUNTOSTOP");
    [HideInInspector] public int _JUMPUP = Animator.StringToHash("JUMPUP");
    [HideInInspector] public int _JUMPDOWN = Animator.StringToHash("JUMPDOWN");
    [HideInInspector] public int _LOCOMOTION = Animator.StringToHash("Running");
    [HideInInspector] public int _SPAWN = Animator.StringToHash("Spawn");
    #endregion

    [HideInInspector] public AbilityControl abilityControl;

    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isArrived = true;
    public Transform eyePoint;
    [ReadOnly] public Transform model;

    public float isGroundedOffset = 1.1f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;

    public CinemachineVirtualCameraBase maincamera;

    [HideInInspector] public ActionGameInput actionInput;
    private ActionGameInput.PlayerActions playerActions;

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

        actionInput = new ActionGameInput();
        playerActions = actionInput.Player;
    }

    void Start()
    {
        Visible(false);
        // foreach (var dat in initialAbilities)
        // {
        //     abilityControl.AddAbility(dat, true);
        // }
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, isGroundedOffset);
    }

    void OnDestroy()
    {
        actionInput.Dispose();
    }

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }

    public void Visible(bool b)
    {
        model.gameObject.SetActive(b);
    }
    
    public void Animate(int hash, float duration, int layer = 0)
    {
        animator?.CrossFadeInFixedTime(hash,duration,layer,0f);
    }
}
