using System.Collections;
using UnityEngine;

public class SpawnerPlayer : Spawner
{
    [SerializeField] EventPlayerSpawnBefore eventPlayerSpawnBefore;
    [SerializeField] EventPlayerSpawnAfter eventPlayerSpawnAfter;

    

    CharacterControl cc;

    CursorControl cursor;
    void OnEnable()
    {
        // 이벤트가 등록되면 발동, 등록 안하면 작동 안함(트리거 역할)
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
        cc = Instantiate(e.playerCC, spawnPoint.position, rot, null);
        cursor = Instantiate(e.playerCursor);
        cc.Profile=actorProfile;
        cursor.EyePoint = cc.eyePoint;

        StartCoroutine(SpawnAfter());
        //GameManager.I.DelayCallAsync(1000,()=>eventPlayerSpawnAfter.Raise());
    }

    IEnumerator SpawnAfter()
    {
        yield return new WaitForEndOfFrame();

        eventPlayerSpawnAfter.eyePoint = cc.eyePoint;
        eventPlayerSpawnAfter.cursorPoint = cursor.EyePoint;
        eventPlayerSpawnAfter.cc=cc;
        eventPlayerSpawnAfter.Raise();
    }
}
