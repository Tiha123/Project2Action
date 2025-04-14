using UnityEngine;
using CustomInspector;
using System.Collections.Generic;

public class SensorControl : MonoBehaviour
{
    [ReadOnly] public CharacterControl target;
    [ReadOnly] public CharacterControl _prevSight;
    [ReadOnly] public CharacterControl _prevAttack;

    public CharacterControl ownerCC;

    [Tooltip("시야범위")]
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;

    [SerializeField] LayerMask targetLayer;//목표
    [SerializeField] LayerMask blockLayer;//장애물
    [SerializeField] string targetTag;//목표 태그

    bool isVisible;
    private float distance;
    Dictionary<CharacterControl, bool> visibleStates=new Dictionary<CharacterControl, bool>();

    public EventSensorSightEnter eventSensorSightEnter;

    public EventSensorSightExit eventSensorSightExit;

    public EventSensorAttackEnter eventSensorAttackEnter;
    public EventSensorAttackExit eventSensorAttackExit;

    void Start()
    {
        if (TryGetComponent(out ownerCC) == false)
        {
            Debug.LogWarning("SensorControl ] CharacterControl 없음");
        }
        InvokeRepeating("CheckOverlap", 0.1f, 0.5f);
    }

    void CheckOverlap()
    {
        Collider[] overlaps = Physics.OverlapSphere(ownerCC.eyePoint.position, sightRange, targetLayer);
        foreach(var c in overlaps)
        {
            if(c.tag != targetTag)
            {
                continue;
            }

            target=c.GetComponentInParent<CharacterControl>();
            if(target==null)
            {
                Debug.LogError("SensorControl ] CharacterControl 없음");
            }

            distance = Vector3.Distance(ownerCC.eyePoint.position, target.eyePoint.position);
            Vector3 dir = (target.eyePoint.position-ownerCC.eyePoint.position).normalized;

            isVisible = !Physics.Raycast(ownerCC.eyePoint.position, dir, distance, blockLayer);
            bool wasVisible;
            visibleStates.TryGetValue(target, out wasVisible);

            if(visibleStates.ContainsKey(target)==false)
            {
                visibleStates[target]=isVisible;
                if(isVisible)
                {
                    OnFound();        
                }
                else
                {
                    OnBlocked();
                }
            }
        }
    }

    private void OnFound()
    {
        Debug.Log("Found");
    }

    private void OnBlocked()
    {
        Debug.Log("Blocked");
    }

    private void OnLost()
    {
        Debug.Log("Lost");
    }

    // void SightEnter()
    // {
    //     if (target == null || _prevSight == target)
    //     {
    //         return;
    //     }
    //     _prevSight = target;
    //     eventSensorSightEnter.from = ownerCC;
    //     eventSensorSightEnter.to = target;
    //     eventSensorSightEnter?.Raise();
    // }

    // void SightExit()
    // {
    //     if (target == null || _prevSight == null)
    //     {
    //         return;
    //     }
    //     _prevSight = null;
    //     eventSensorSightExit.from = ownerCC;
    //     eventSensorSightExit.to = eventSensorSightEnter.to;
    //     eventSensorSightExit?.Raise();
    // }

    // // 공격 범위 진입 시
    // private void AttackEnter()
    // {
    //     if (target == null || _prevAttack == target)
    //     {
    //         return;
    //     }
    //     _prevAttack = target;

    //     eventSensorAttackEnter.from = ownerCC;
    //     eventSensorAttackEnter.to = target;
        
    //     eventSensorAttackEnter?.Raise();
    // }

    // private void AttackExit()
    // {
    //     if (target == null || _prevAttack == null)
    //     {
    //         return;
    //     }
    //     _prevAttack = null;
    //     eventSensorAttackExit.from = ownerCC;
    //     eventSensorAttackExit.to = eventSensorAttackEnter.to;
    //     eventSensorAttackExit?.Raise();
    // }
    // //오브젝트를 선택했을 때만 기즈모
    // void OnDrawGizmosSelected()
    // {
    //     if(target==null)
    //     {
    //         return;
    //     }
    //     Gizmos.color= isVisible ? Color.blue : Color.red;
    //     Gizmos.DrawLine(ownerCC.eyePoint.position, target.eyePoint.position);
    // }

}
