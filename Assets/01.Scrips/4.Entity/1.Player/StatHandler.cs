using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatHandler : MonoBehaviour,IBattleEntity
{
    public StatData statData;
    private Dictionary<StatType, int> currentStats = new Dictionary<StatType, int>();
    public Dictionary<StatType, int>  serializeStats = new Dictionary<StatType, int>();// 세이브용 스탯 저장할 딕셔너리
    
    public PlayerInfo playerInfo;
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
    
    public Dictionary<StatType, int> ToStatDict()
    {
        var dict = new Dictionary<StatType, int>();
        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            dict[stat] = GetStat(stat);
        }
        return dict;
    }

    public void LoadStatsToCurrent()
    {
        foreach(KeyValuePair<StatType, int> items in serializeStats)
        {
            currentStats[items.Key] = items.Value;
        }
    }
/*============================================================================================*/

    public IEnumerator ActionOnTurn(BattlePhase phase)
    {
        return null;
    }

    public void GetDamage(int dmg)
    { 
        int Rd = Random.Range(0, 100);

        if (GetStat(StatType.Evasion) > Rd)
        {
           UIManager.Instance.SystemMessage("공격을 회피했습니다."); 
            return;
        }
        
       playerInfo.currentHp -= dmg;
        Die();
    }

    public bool isDead { get; private set; }

    void Die()
    {
        if (playerInfo.currentHp<=0)
        {
            isDead = true;
        };

    }

    public EntityInfo GetEntityInfo()
    {
        throw new NotImplementedException();
    }
}