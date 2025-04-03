using CustomInspector;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region EVENTS
    [Space(20)]
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventPlayerSpawnBefore eventPlayerSpawnBefore;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    [SerializeField] ActorProfile actorProfile;
    #endregion
    [HorizontalLine(color: FixedColor.Blue), HideField] public bool _l1;

    [Space(20)]
    public Transform spawnPoint;
    public float radius = 2f;
    public float lineLength = 4f;


    void OnEnable()
    {
        eventPlayerSpawnBefore.Register(OneventPlayerSpawned);
    }

    void OnDisable()
    {
        eventPlayerSpawnBefore.Unregister(OneventPlayerSpawned);
    }

    void Start()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * lineLength);
    }

    void OneventPlayerSpawned(EventPlayerSpawnBefore e)
    {
        if (e.playerCC == null || e.playerCursor == null || e.playerCamera == null)
        {
            return;
        }
        CameraControl camera = Instantiate(e.playerCamera);
        CharacterControl cc = Instantiate(e.playerCC);
        Quaternion rot = Quaternion.LookRotation(spawnPoint.forward);
        cc.transform.SetPositionAndRotation(spawnPoint.position, rot);

        CursorControl cursor = Instantiate(e.playerCursor);
        cursor.EyePoint = cc.eyePoint;

        eventPlayerSpawnAfter.eyePoint = cc.eyePoint;
        eventPlayerSpawnAfter.cursorPoint = cursor.EyePoint;
        eventPlayerSpawnAfter.actorProfile = actorProfile;
        eventPlayerSpawnAfter.Raise();
        //GameManager.I.DelayCallAsync(1000,()=>eventPlayerSpawnAfter.Raise());

    }
}
