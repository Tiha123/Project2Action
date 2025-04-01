using System.Collections;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public EventPlayerSpawnBefore eventPlayerSpawnBefore;

    public EventPlayerSpawnAfter eventPlayerSpawnAfter;

    void Start()
    {
        eventPlayerSpawnBefore?.Raise();
    }

}
