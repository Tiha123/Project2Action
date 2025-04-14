using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class SensorOld : MonoBehaviour
{
    [Header("Detection Settings")]
    public float interval = 0.5f; // Interval for detection checks
    public float detectionRadius = 5f;
    public float attackRange = 2f;
    public float fieldOfViewAngle = 60f; // New FOV angle parameter
    public LayerMask targetLayer;
    public LayerMask blockLayer;
    public string targetTag = "Enemy";

    [SerializeField] SensorFOV sensorFOV;

    CharacterControl target;
    CharacterControl owner;

    public bool showGizmos = true; 


    [Header("Target Events")]
    private Dictionary<CharacterControl, bool> visibilityStates = new Dictionary<CharacterControl, bool>();

    public EventSensorSightEnter eventSensorSightEnter;

    public EventSensorSightExit eventSensorSightExit;

    public EventSensorAttackEnter eventSensorAttackEnter;
    public EventSensorAttackExit eventSensorAttackExit;

    void Start()
    {
        owner= GetComponentInParent<CharacterControl>();
        InvokeRepeating("DetectTargets", 0f, interval);
    }

    void DetectTargets()
    {
        HashSet<CharacterControl> currentFrameTargets = new HashSet<CharacterControl>();
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag(targetTag))
                continue;

            target = hit.GetComponentInParent<CharacterControl>();
            if(target==null)
            {
                Debug.LogWarning($"Sensor ] target - CharacterControl 없음");
            }

            Vector3 direction = (target.eyePoint.position - transform.position).normalized;
            
            // New FOV angle check
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle > (fieldOfViewAngle * 0.5f))
                continue;

            currentFrameTargets.Add(target);

            float distance = Vector3.Distance(transform.position, target.eyePoint.position);
            bool isVisible = !Physics.Raycast(transform.position, direction, distance, blockLayer);

            visibilityStates.TryGetValue(target, out bool wasVisible);
            
            if (!visibilityStates.ContainsKey(target))
            {
                visibilityStates[target] = isVisible;
                
            }
            else if (wasVisible != isVisible)
            {
                visibilityStates[target] = isVisible;
                
            }
        }

        List<CharacterControl> toRemove = new List<CharacterControl>();
        foreach (var kvp in visibilityStates)
        {
            if (!currentFrameTargets.Contains(kvp.Key))
            {
                
            }
        }

        foreach (var t in toRemove)
            visibilityStates.Remove(t);
    }

    void TriggerEvent(UnityEvent<Transform> unityEvent, Transform target)
    {
        unityEvent?.Invoke(target);
    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;
        
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (visibilityStates == null)
            return;

        // Draw FOV cone visualization
        if (fieldOfViewAngle > 0)
        {
            Gizmos.color = Color.cyan;

            Vector3 forwardDir = transform.forward.normalized;
            Vector3 forwardEnd = transform.position + forwardDir * detectionRadius;

            // Draw central forward line
            Gizmos.DrawLine(transform.position, forwardEnd);

            // Calculate left and right directions
            float halfAngle = fieldOfViewAngle * 0.5f;
            Vector3 rightDir = Quaternion.AngleAxis(-halfAngle, Vector3.up) * forwardDir;
            Vector3 leftDir = Quaternion.AngleAxis(halfAngle, Vector3.up) * forwardDir;

            Vector3 leftEnd = transform.position + leftDir * detectionRadius;
            Vector3 rightEnd = transform.position + rightDir * detectionRadius;

            // Draw left and right edges
            Gizmos.DrawLine(transform.position, leftEnd);
            Gizmos.DrawLine(transform.position, rightEnd);

            // Connect the ends
            Gizmos.DrawLine(leftEnd, rightEnd);
        }

        foreach (var pair in visibilityStates)
        {
            CharacterControl target = pair.Key;
            bool isVisible = pair.Value;

            if (target == null) continue;

            Gizmos.color = isVisible ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, target.eyePoint.position);
        }
    }private void OnFound()
    {
        sensorFOV?.AlertColor(true);
        eventSensorSightEnter.from = owner;
        eventSensorSightEnter.to=target;
        eventSensorSightEnter?.Raise();
    }

    private void OnBlocked()
    {
        sensorFOV?.AlertColor(false);
        eventSensorSightExit.from = owner;
        eventSensorSightExit.to=target;
        eventSensorSightExit?.Raise();
    }

    private void OnLost()
    {
        sensorFOV?.AlertColor(false);
        eventSensorSightExit.from = owner;
        eventSensorSightExit.to=target;
        eventSensorSightExit?.Raise();
    }
}