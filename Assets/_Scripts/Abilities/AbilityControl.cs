using System.Collections.Generic;
using System.Linq;
using CustomInspector;
using UnityEngine;
using UnityEngine.InputSystem;

// abiltyDatas 외부에서 능력 부여/회수 인터페이스
// abilities abilityDatas에서 갱신해서 행동
public class AbilityControl : MonoBehaviour
{
    [Title("Event", fontSize = 15, alignment = TextAlignment.Center), HideField] bool _h0;

    [Title("Ability System", fontSize = 15, alignment = TextAlignment.Center), HideField] bool _h1;
    [Space(20), ReadOnly] public AbilityFlag Flags = AbilityFlag.None;
    [Space(10), SerializeField] List<AbilityData> datas = new List<AbilityData>();

    private readonly Dictionary<AbilityFlag, Ability> actives = new Dictionary<AbilityFlag, Ability>();

    public void AddAbility(AbilityData d, bool immediate = false)
    {
        if (datas.Contains(d))
        {
            return;
        }
        Flags.Add(d.Flag, null);

        datas.Add(d);

        var ability = d.CreateAbility(GetComponent<CharacterControl>());

        if (immediate)
        {
            actives[d.Flag] = ability;
            ability.Activate();
        }
    }
    public void RemoveAbility(AbilityData d)
    {
        if (datas.Contains(d) == false || d == null)
        {
            return;
        }
        Deactivate(d.Flag);
        datas.Remove(d);
        Flags.Remove(d.Flag, null);
        actives.Remove(d.Flag);
    }

    void Update()
    {
        foreach (var v in actives.ToList())
        {
            v.Value?.Update();
        }
    }

    void FixedUpdate()
    {
        foreach (var v in actives.ToList())
        {
            v.Value?.FixedUpdate();
        }
    }

    public void Activate(AbilityFlag flag,  bool forcemode, object obj)
    {
        if(forcemode==true)
        {
            DeactivateAll();
        }
        // 실시간 삭제된 데이터 반영
        List<AbilityData> temp = new List<AbilityData>();
        temp.AddRange(datas.GetRange(0,datas.Count));

        foreach (var d in temp)
        {
            if ((d.Flag & flag) == flag)
            {
                if (actives.ContainsKey(flag) == false)
                {
                    actives[flag] = d.CreateAbility(GetComponent<CharacterControl>());
                }
                // HashSet<> 중복 X 자동정렬
                actives[flag].Activate(obj);
            }
        }
    }

    public void Deactivate(AbilityFlag flag)
    {
        foreach (var d in datas)
        {
            if ((d.Flag & flag) == flag)
            {
                if (actives.ContainsKey(flag) == true)
                {
                    actives[flag].Deactivate();
                    actives[flag] = null;
                    actives.Remove(flag);
                }
            }
        }
    }

    public void DeactivateAll()
    {
        foreach (var a in actives)
        {
            a.Value.Deactivate();
        }
        actives.Clear();
    }

    public void RemoveALL()
    {
        Flags=AbilityFlag.None;
        DeactivateAll();
        datas.Clear();
        actives.Clear();
    }
}
