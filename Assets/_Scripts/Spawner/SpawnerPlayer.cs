using UnityEngine;

public class SpawnerPlayer : Spawner
{
    [SerializeField] EventPlayerSpawnBefore eventPlayerSpawnBefore;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;
    void OnEnable()
    {
        // 이벤트가 등록되면 발동, 등록 안하면 작돋ㅇ한함(트리거 열할)
        eventPlayerSpawnBefore?.Register(OneventPlayerSpawnBefore);

    }
    void OnDisable()
    {
        eventPlayerSpawnBefore?.Unregister(OneventPlayerSpawnBefore);

    }
    void OneventPlayerSpawnBefore(EventPlayerSpawnBefore e)
    {
        if (e.playerCC == null || e.playerCursor == null || e.playerCamera == null)
        {
            return;
        }
        CameraControl camera = Instantiate(e.playerCamera);
        Quaternion rot = Quaternion.LookRotation(spawnPoint.forward);
        CharacterControl cc = Instantiate(e.playerCC, spawnPoint.position, rot, null);
        CursorControl cursor = Instantiate(e.playerCursor);
        cursor.EyePoint = cc.eyePoint;

        eventPlayerSpawnAfter.eyePoint = cc.eyePoint;
        eventPlayerSpawnAfter.cursorPoint = cursor.EyePoint;
        eventPlayerSpawnAfter.actorProfile = actorProfile;
        eventPlayerSpawnAfter.Raise();
        //GameManager.I.DelayCallAsync(1000,()=>eventPlayerSpawnAfter.Raise());

    }
}
