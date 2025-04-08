using UnityEngine;
using CustomInspector;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

public class EnemyEventControl : MonoBehaviour
{
    [HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l0;
    [SerializeField] EventEnemySpawnAfter eventEnemySpawnAfter;

    [Space(10), HorizontalLine("Events", color: FixedColor.Blue), HideField] public bool _l1;

    public CharacterControl cc;

    void Awake()
    {
        if (TryGetComponent(out cc) == false)
        {
            Debug.LogWarning($"GameEventControl ] CharacterControl 없음");
        }
    }

    void OnEnable()
    {
        eventEnemySpawnAfter?.Register(OneventEnemySpawnAfter);
    }

    void OnDisable()
    {
        eventEnemySpawnAfter?.Unregister(OneventEnemySpawnAfter);
    }

    void OneventEnemySpawnAfter(EventEnemySpawnAfter e)
    {
        StartCoroutine(SpawnSequence(e));
    }

    IEnumerator SpawnSequence(EventEnemySpawnAfter e)
    {
        cc.Profile = e.actorProfile;
        if (e.actorProfile.model == null)
        {
            Debug.LogError("모델 없음");
        }
        var clone = Instantiate(e.actorProfile.model, cc.model);
        clone.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().ForEach(m =>
        {
            m.gameObject.layer = LayerMask.NameToLayer("Silhouette");
        });


        if (e.actorProfile.avatar == null)
        {
            Debug.LogError("아바타 없음");
        }
        cc.animator.avatar = e.actorProfile.avatar;
        yield return new WaitForSeconds(1f);
        cc.Visible(true);

        foreach(var dat in cc.Profile.initialAbilities)
        {
            cc.abilityControl.AddAbility(dat);
        }
    }




    //비동기(Async)
    // 1. 코루틴 (Co-routine)
    // 2. Invoke
    // 3. async / await
    // 4. Awaitable
    // 5. CySharp-UniTask
}
