using UnityEngine;
using CustomInspector;
using System.Linq;

public class SensorControl : MonoBehaviour
{
    [ReadOnly] public CharacterControl target;
    [ReadOnly] public CharacterControl _prev;

    public CharacterControl ownerCC;

    [Tooltip("시야범위")]
    public float sightRange;

    [SerializeField] LayerMask layer;
    [SerializeField] string tagname;

    public EventSensorTargetEnter eventSensorTargetEnter;

    public EventSensorTargetExit eventSensorTargetExit;

    void Start()
    {
        if (TryGetComponent(out ownerCC) == false)
        {
            Debug.LogWarning("SensorControl ] CharacterControl 없음");
        }
    }

    bool b0;
    void Update()
    {
        Collider[] overlaps = Physics.OverlapSphere(ownerCC.eyePoint.position, sightRange, layer);
        b0=false;
        overlaps.ToList().ForEach(v =>
        {
            if (v.gameObject.tag == tagname)
            {
                target = v.GetComponentInParent<CharacterControl>();
                TargetEnter();
                b0=true;
                return;
            }
        }
        );
        if(b0==false)
        {
            TargetExit();
        }

    }

    void TargetEnter()
    {
        if(target==null||_prev==target)
        {
            return;
        }
        _prev=target;
        eventSensorTargetEnter.from=ownerCC;
        eventSensorTargetEnter.to=target;
            eventSensorTargetEnter?.Raise();
    }

    void TargetExit()
    {
        if(target==null||_prev==null)
        {
            return;
        }
        _prev=null;
        eventSensorTargetExit.from=ownerCC;
        eventSensorTargetExit.to=eventSensorTargetEnter.to;
            eventSensorTargetExit?.Raise();
    }

}
