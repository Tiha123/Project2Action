using System.Collections;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public EventPlayerSpawnBefore eventPlayerSpawnBefore;
    public EventEnemySpawnBefore eventEnemySpawnBefore;
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
