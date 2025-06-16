using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class StatHandler : MonoBehaviour
{
    public StatData statData;
    private Dictionary<StatType, int> currentStats = new Dictionary<StatType, int>();
    public Dictionary<string, string>  serializeStats = new Dictionary<string, string>();// 세이브용 스탯 저장할 딕셔너리
    
   
    private void Awake()
    {
        InitializeStats();
       
    }

    private void InitializeStats()
    {
        foreach (StatEntry entry in statData.stats)
        {
            currentStats[entry.statType] = entry.baseValue;
        }
    }

    public int GetStat(StatType statType)
    {
        return currentStats.ContainsKey(statType) ? currentStats[statType] : 0;
    }

    public void ModifyStat(StatType statType, int amount, bool isPermanent = true, float duration = 0)
    {
        if (!currentStats.ContainsKey(statType)) return;

        currentStats[statType] += amount;

        if (!isPermanent)
        {
            StartCoroutine(RemoveStatAfterDuration(statType, amount, duration));
        }
    }

    

    private IEnumerator RemoveStatAfterDuration(StatType statType, int amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentStats[statType] -= amount;
    }
    public void SetStat(StatType statType, int setvalue)
    {
        if (!currentStats.ContainsKey(statType)) return;

        currentStats[statType] = setvalue;
    }
    
    //죄송해요 애초에 다 스트링으로 해야되는데 잘못만들었습니다. 
    //요기에 만들어둔 스트링 딕셔너리는 사용하셔도되고 안하셔도됩니다. 
    //키값 = 타입 밸류값 = 스탯값 요렇게 들어가게 해두었습니다 .
    // serializeStats(또는 세이브쪽에서 사용하실 딕셔너리 만드셔서) =ToStarDict();
    //스탯 핸들러자체는 GameManager -> player -> statHandler 요렇게 캐싱해 두었습니다.
    public Dictionary<string, string> ToStatDict()
    {
        var dict = new Dictionary<string, string>();
        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            dict[stat.ToString()] = GetStat(stat).ToString();
        }
        return dict;
    }
    
/*============================================================================================*/

   
}