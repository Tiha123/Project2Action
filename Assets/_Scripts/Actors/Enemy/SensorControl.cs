using UnityEngine;
using CustomInspector;
using System.Linq;

public class SensorControl : MonoBehaviour
{
    [ReadOnly] public CharacterControl target;
    [ReadOnly] public CharacterControl _prevSight;
    [ReadOnly] public CharacterControl _prevAttack;

    public CharacterControl ownerCC;

    [Tooltip("시야범위")]
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;

    [SerializeField] LayerMask layer;
    [SerializeField] string tagname;

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
    }

    bool b0=false;
    void Update()
    {
        Collider[] overlaps = Physics.OverlapSphere(ownerCC.eyePoint.position, sightRange, layer);
        b0 = false;
        overlaps.ToList().ForEach(v =>
        {
            if (v.gameObject.tag == tagname)
            {
                target = v.GetComponentInParent<CharacterControl>();
                SightEnter();

                var d = Vector3.Distance(target.eyePoint.position, ownerCC.eyePoint.position);
                if (d <= attackRange)
                {
                    AttackEnter();
                }
                else
                {
                    AttackExit();
                }

                b0 = true;
                return;

            }
        }
        );
        if (b0 == false)
        {
            SightExit();
        }

    }

    void SightEnter()
    {
        if (target == null || _prevSight == target)
        {
            return;
        }
        _prevSight = target;
        eventSensorSightEnter.from = ownerCC;
        eventSensorSightEnter.to = target;
        eventSensorSightEnter?.Raise();
    }

    void SightExit()
    {
        if (target == null || _prevSight == null)
        {
            return;
        }
        _prevSight = null;
        eventSensorSightExit.from = ownerCC;
        eventSensorSightExit.to = eventSensorSightEnter.to;
        eventSensorSightExit?.Raise();
    }

    // 공격 범위 진입 시
    private void AttackEnter()
    {
        if (target == null || _prevAttack == target)
        {
            return;
        }
        _prevAttack = target;

        eventSensorAttackEnter.from = ownerCC;
        eventSensorAttackEnter.to = target;
        
        eventSensorAttackEnter?.Raise();
    }

    private void AttackExit()
    {
        if (target == null || _prevAttack == null)
        {
            return;
        }
        _prevAttack = null;
        eventSensorAttackExit.from = ownerCC;
        eventSensorAttackExit.to = eventSensorAttackEnter.to;
        eventSensorAttackExit?.Raise();
    }

}
