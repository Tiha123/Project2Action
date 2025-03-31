using System.Collections.Generic;
using UnityEngine;
using CustomInspector;
using Unity.Cinemachine;
using Project2Action;
using UnityEngine.InputSystem;

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
    #endregion

    [HideInInspector] public AbilityControl abilityControl;
    public List<AbilityData> initialAbilities = new List<AbilityData>();

    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isArrived = true;

    public float isGroundedOffset = 1.1f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;

    public CinemachineVirtualCameraBase maincamera;

    [HideInInspector] public ActionGameInput actionInput;
    private ActionGameInput.PlayerActions playerActions;
    InputAction.CallbackContext context;

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

        actionInput = new ActionGameInput();
        playerActions = actionInput.Player;
    }

    void Start()
    {
        foreach (var dat in initialAbilities)
        {
            abilityControl.AddAbility(dat, true);
        }
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
}
