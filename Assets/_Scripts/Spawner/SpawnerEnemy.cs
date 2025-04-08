using System.Collections;
using UnityEngine;

public class SpawnerEnemy : Spawner
{
    [SerializeField] EventEnemySpawnBefore eventEnemySpawnBefore;
    [SerializeField] EventEnemySpawnAfter eventEnemySpawnAfter;
    CharacterControl cc;
    void OnEnable()
    {
        eventEnemySpawnBefore?.Register(OneventEnemySpawnBefore);
    }

    void OnDisable()
    {
        eventEnemySpawnBefore?.Unregister(OneventEnemySpawnBefore);

    }

    void OneventEnemySpawnBefore(EventEnemySpawnBefore e)
    {
        Quaternion rot = Quaternion.LookRotation(spawnPoint.forward);
        cc = Instantiate(e.EnemyCC, spawnPoint.position, rot, null);

        StartCoroutine(SpawnAfter());
    }

    IEnumerator SpawnAfter()
    {
        yield return new WaitForEndOfFrame();
        eventEnemySpawnAfter.actorProfile = actorProfile;
        eventEnemySpawnAfter.eyePoint = cc.eyePoint;
        eventEnemySpawnAfter?.Raise();
    }
}
