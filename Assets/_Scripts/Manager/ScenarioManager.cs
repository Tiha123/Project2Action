using System.Collections;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public EventPlayerSpawnBefore eventPlayerSpawnBefore;

    public EventPlayerSpawnAfter eventPlayerSpawnAfter;
    public EventEnemySpawnBefore eventEnemySpawnBefore;
    public EventEnemySpawnAfter eventEnemySpawnAfter;

    IEnumerator Start()
    {
        eventPlayerSpawnBefore?.Raise();
        yield return new WaitForEndOfFrame();
        eventEnemySpawnBefore?.Raise();
    }

}
