using System.Collections;
using System.Collections.Generic;
using CustomInspector;
using UnityEngine;

// abiltyDatas 외부에서 능력 부여/회수 인터페이스
// abilities abilityDatas에서 갱신해서 행동
public class AbilityControl : MonoBehaviour
{
    [Title("Ability System",fontSize =15, alignment =TextAlignment.Center), HideField] bool _h0;
    [Space(20), ReadOnly]public AbilityFlag Flags=AbilityFlag.None;
    [Space(10), SerializeField] List<AbilityData> datas = new List<AbilityData>();

    private readonly Dictionary<AbilityFlag, Ability> actives=new Dictionary<AbilityFlag, Ability>();

    public void AddAbility(AbilityData d, bool immediate)
    {
        if(datas.Contains(d))
        {
            return;
        }
        datas.Add(d);
        var ability=d.CreateAbility(this.transform);
        Flags.Add(d.Flag,null);
        if(immediate)
        {
            actives[d.Flag]=ability;
        }
    }
    public void RemoveAbility(AbilityData d)
    {
        if(datas.Contains(d)==false || d==null)
        {
            return;
        }
        datas.Remove(d);
        Flags.Remove(d.Flag,null);
        actives.Remove(d.Flag);
    }

    void Update()
    {
        foreach(var v in actives)
        {
            v.Value.Update();
        }
    }

    [Space(10)] public AbilityData testdata;
    [Space(10)] public AbilityData testdata2;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame(); 
        AddAbility(testdata, true);
        AddAbility(testdata2, true);
    }
}
