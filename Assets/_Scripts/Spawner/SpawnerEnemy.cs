using System.Collections;
using UnityEngine;

public class SpawnerEnemy : Spawner
{
    [SerializeField] EventEnemySpawnBefore eventEnemySpawnBefore;
    [SerializeField] EventEnemySpawnAfter eventEnemySpawnAfter;
    void OnEnable()
    {
        eventEnemySpawnBefore?.Register(OneventEnemySpawnBefore);
    }

    void OnDisable()
    {
        eventEnemySpawnBefore?.Unregister(OneventEnemySpawnBefore);

    }

    EnemyControl _enemy;
    void OneventEnemySpawnBefore(EventEnemySpawnBefore e)
    {
        Quaternion rot = Quaternion.LookRotation(spawnPoint.forward);
        _enemy = Instantiate(e.EnemyEC, spawnPoint.position, rot, null);

        StartCoroutine(SpawnAfter());
    }

    IEnumerator SpawnAfter()
    {
        yield return new WaitForEndOfFrame();
        eventEnemySpawnAfter.actorProfile = actorProfile;
        eventEnemySpawnAfter.eyePoint = _enemy.eyePoint;
        eventEnemySpawnAfter?.Raise();
    }
}
