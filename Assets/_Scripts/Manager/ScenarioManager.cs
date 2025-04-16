using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public EventPlayerSpawnBefore eventPlayerSpawnBefore;
    public EventPlayerSpawnAfter eventPlayerSpawnAfter;
    public EventEnemySpawnBefore eventEnemySpawnBefore;
    public EventEnemySpawnAfter eventEnemySpawnAfter;
    public EventDeath eventDeath;

    void OnEnable()
    {
        eventDeath.Register(OneventDeath);
    }

    void OnDisable()
    {
        eventDeath.Unregister(OneventDeath);
    }

    IEnumerator Start()
    {
        eventPlayerSpawnBefore?.Raise();
        yield return new WaitForEndOfFrame();
        eventEnemySpawnBefore?.Raise();
    }

    void OneventDeath(EventDeath e)
    {
        if(e.target.Profile.actorType==ActorType.Player)
        {
            GameManager.I.ShowInfo("You Died", 10f);
        }
    }

}
