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
        Vector3 rndpos= spawnPoint.position+new Vector3(Random.Range(-radius,radius),0f,Random.Range(-radius,radius));
        Quaternion rot = Quaternion.LookRotation(spawnPoint.forward);
        
        cc = Instantiate(e.EnemyCC, rndpos, rot, null);
        cc.Profile=actorProfile;
        cc.state.Set(actorProfile);

        StartCoroutine(SpawnAfter());
    }

    IEnumerator SpawnAfter()
    {
        yield return new WaitForEndOfFrame();
        eventEnemySpawnAfter.cc=cc;
        eventEnemySpawnAfter.eyePoint = cc.eyePoint;
        eventEnemySpawnAfter?.Raise();
    }
}
