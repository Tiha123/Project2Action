using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

public struct TargetState
{
    public bool isVisible;
    public bool isArrived;
}

public class Sensor : MonoBehaviour
{
    [Header("Detection Settings")]
    public float interval = 0.3f; // Interval for detection checks
    public float detectionRadius = 5f;
    public float fieldOfViewAngle = 60f; // New FOV angle parameter
    public LayerMask targetLayer;
    public LayerMask blockLayer;
    public string targetTag = "Enemy";
    public float attackRadius = 3f; // 공격 반경
    public bool showGizmos = true;
    [ReadOnly] private CharacterControl owner, target;

    [Header("Target Events")]
    [SerializeField] EventEnemySpawnAfter eventEnemySpawnAfter;
    [SerializeField] EventSensorSightEnter eventSensorSightEnter;
    [SerializeField] EventSensorSightExit eventSensorSightExit;
    [SerializeField] EventSensorAttackEnter eventSensorAttackEnter;
    [SerializeField] EventSensorAttackExit eventAttackExit;
    
    void OnEnable()
    {
        eventEnemySpawnAfter.Register(OneventEnemySpawnAfter);
    }
    void OnDisable()
    {
        eventEnemySpawnAfter.Unregister(OneventEnemySpawnAfter);
    }
    void OneventEnemySpawnAfter(EventEnemySpawnAfter e)
    {
        if(owner!=e.cc)
        {
            return;
        }
        detectionRadius=owner.Profile.sightRange;
        attackRadius=owner.Profile.attackRange;
        
    }

    private Dictionary<CharacterControl, TargetState> visibilityStates = new Dictionary<CharacterControl, TargetState>();

    void Start()
    {
        owner=GetComponentInParent<CharacterControl>();
        if(owner==null)
        {
            Debug.LogError($"Sensor ] owner - CharacterControl 없음");
        }
        InvokeRepeating("DetectTargets", 0f, interval);
    }

    void DetectTargets()
    {
        HashSet<CharacterControl> currentFrameTargets = new HashSet<CharacterControl>();
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);
        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag(targetTag))
            {

                continue;
            }
            target = hit.GetComponentInParent<CharacterControl>();
            if(target==null)
            {
                Debug.LogError($"Sensor ] target - CharacterControl 없음");
            }

            if(target.isDamageable==false)
            {
                continue;
            }
            Vector3 direction = (target.eyePoint.position - transform.position).normalized;

            float angle = Vector3.Angle(transform.forward, direction);
            if (angle > (fieldOfViewAngle * 0.5f))
                continue;

            currentFrameTargets.Add(target);

            float distance = Vector3.Distance(transform.position, target.eyePoint.position);//거리
            bool isVisible = !Physics.Raycast(transform.position, direction, distance, blockLayer);//장애물
            bool isArrived = distance <= attackRadius;//공격거리

            visibilityStates.TryGetValue(target, out TargetState previousState);

            TargetState newState = new TargetState
            {
                isVisible = isVisible,
                isArrived = isArrived
            };

            if (!visibilityStates.ContainsKey(target))
            {
                visibilityStates[target] = newState;
                if(isVisible)
                {
                    OnFound();
                }
                else
                {
                    OnBlocked();
                }
                
                if(isArrived)
                {
                    OnArrived();
                }
            }
            else if (previousState.isVisible != isVisible || previousState.isArrived != isArrived)
            {
                visibilityStates[target] = newState;
                if(isVisible)
                {
                    OnFound();
                }
                else
                {
                    OnBlocked();
                }
                
                if(isArrived&&!previousState.isArrived)
                {
                    OnArrived();
                }
            }
        }

        List<CharacterControl> toRemove = new List<CharacterControl>();
        foreach (var kvp in visibilityStates)
        {
            if (!currentFrameTargets.Contains(kvp.Key))
            {
                toRemove.Add(kvp.Key);
                OnLost();
            }
        }

        //실제 삭제
        foreach (var t in toRemove)
            visibilityStates.Remove(t);
    }

    void OnDrawGizmosSelected()
    {
        Debug.Log(1);
        if (!showGizmos) return;
        Debug.Log(2);
        if (visibilityStates == null)
            return;
        Debug.Log(3);
        if (fieldOfViewAngle > 0)
        {
            Gizmos.color = Color.cyan;

            Vector3 forwardDir = transform.forward.normalized;
            Vector3 forwardEnd = transform.position + forwardDir * detectionRadius;

            Gizmos.DrawLine(transform.position, forwardEnd);

            float halfAngle = fieldOfViewAngle * 0.5f;
            Vector3 rightDir = Quaternion.AngleAxis(-halfAngle, Vector3.up) * forwardDir;
            Vector3 leftDir = Quaternion.AngleAxis(halfAngle, Vector3.up) * forwardDir;

            Vector3 leftEnd = transform.position + leftDir * detectionRadius;
            Vector3 rightEnd = transform.position + rightDir * detectionRadius;

            Gizmos.DrawLine(transform.position, leftEnd);
            Gizmos.DrawLine(transform.position, rightEnd);

            Gizmos.DrawLine(leftEnd, rightEnd);
        }

        foreach (var pair in visibilityStates)
        {
            CharacterControl target = pair.Key;
            TargetState state = pair.Value;

            if (target == null) continue;

            Gizmos.color = state.isVisible ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, target.eyePoint.position);
        }
    }

    void OnFound()
    {
        owner.ui.Display("Found");
        eventSensorSightEnter.from=owner;
        eventSensorSightEnter.to=target;
        eventSensorSightEnter.Raise();
    }

    void OnBlocked()
    {
        owner.ui.Display("Blocked");
        eventSensorSightExit.from=owner;
        eventSensorSightExit.to=target;
        eventSensorSightExit.Raise();
    }

    void OnLost()
    {
        owner.ui.Display("Lost");
        eventSensorSightExit.from=owner;
        eventSensorSightExit.to=target;
        eventSensorSightExit.Raise();
    }

    void OnArrived()
    {
        owner.ui.Display("Arrived");
        eventSensorAttackEnter.from=owner;
        eventSensorAttackEnter.to=target;
        eventSensorAttackEnter.Raise();
    }
}