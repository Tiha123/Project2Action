using Unity.Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    #region EVENTS
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    #endregion

    [SerializeField] CinemachineTargetGroup targetGroup;

    void OnEnable()
    {
        eventPlayerSpawnAfter?.Register(OneventPlayerSpawn);
    }

    void OnDisable()
    {
        eventPlayerSpawnAfter.Unregister(OneventPlayerSpawn);
        targetGroup.Targets.Clear();
    }

    void OneventPlayerSpawn(EventPlayerSpawnAfter e)
    {
        targetGroup.Targets.Clear();
        CinemachineTargetGroup.Target main= new CinemachineTargetGroup.Target();
        CinemachineTargetGroup.Target sub= new CinemachineTargetGroup.Target();
        main.Object=eventPlayerSpawnAfter.eyePoint;
        main.Weight=1f;
        sub.Object=eventPlayerSpawnAfter.cursorPoint;
        sub.Weight=0.1f;
        targetGroup.Targets.Add(main);
        targetGroup.Targets.Add(sub);
    }
}
