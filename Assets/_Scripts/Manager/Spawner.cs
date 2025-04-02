using CustomInspector;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    #region EVENTS
    [Space(20)]
    [HorizontalLine("Events", color:FixedColor.Blue), HideField]public bool _l0;
    [SerializeField] EventPlayerSpawnBefore eventPlayerSpawnBefore;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    #endregion

    [HorizontalLine(color:FixedColor.Blue), HideField] public bool _l1;

    [Space(20)]
    public float radius = 2f;
    public float lineLength=4f;
    public Transform spawnPoint;


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
        Gizmos.color=Color.red;
        Gizmos.DrawSphere(transform.position, radius);
        Gizmos.color=Color.blue;
        Gizmos.DrawLine(transform.position, transform.position+transform.forward*lineLength);
    }

    void OneventPlayerSpawned(EventPlayerSpawnBefore e)
    {
         if(eventPlayerSpawnBefore.playerCC == null || eventPlayerSpawnBefore.playerCursor == null || eventPlayerSpawnBefore.playerCamera == null)
        {
            return;
        }
        CameraControl camera = Instantiate(eventPlayerSpawnBefore.playerCamera);

        CharacterControl cc = Instantiate(eventPlayerSpawnBefore.playerCC);
        Quaternion rot=Quaternion.LookRotation(transform.forward);
        cc.transform.SetPositionAndRotation(transform.position, rot);

        CursorControl cursor=Instantiate(eventPlayerSpawnBefore.playerCursor);
        cursor.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        cursor.EyePoint=cc.eyePoint;

        eventPlayerSpawnAfter.eyePoint=cc.eyePoint;
        eventPlayerSpawnAfter.cursorPoint=cursor.CursorPoint;
        eventPlayerSpawnAfter.Raise();
        //GameManager.I.DelayCallAsync(1000,()=>eventPlayerSpawnAfter.Raise());
        
    }
}
