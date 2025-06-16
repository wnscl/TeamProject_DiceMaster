using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatHandler : MonoBehaviour,IBattleEntity
{
    public StatData statData;
    private Dictionary<StatType, float> currentStats = new Dictionary<StatType, float>();
    public Dictionary<StatType, float>  serializeStats = new Dictionary<StatType, float>();// 세이브용 스탯 저장할 딕셔너리
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

    public float GetStat(StatType statType)
    {
        return currentStats.ContainsKey(statType) ? currentStats[statType] : 0;
    }

    public void ModifyStat(StatType statType, float amount, bool isPermanent = true, float duration = 0)
    {
        if (!currentStats.ContainsKey(statType)) return;

        currentStats[statType] += amount;

        if (!isPermanent)
        {
            StartCoroutine(RemoveStatAfterDuration(statType, amount, duration));
        }
    }

    

    private IEnumerator RemoveStatAfterDuration(StatType statType, float amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentStats[statType] -= amount;
    }
    public void SetStat(StatType statType, float setvalue)
    {
        if (!currentStats.ContainsKey(statType)) return;

        currentStats[statType] = setvalue;
    }
    
    public Dictionary<string, float> ToStatDict()
    {
        var dict = new Dictionary<string, float>();
        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            dict[stat.ToString()] = GetStat(stat);
        }
        return dict;
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
        
        ModifyStat(StatType.Hp, -dmg);
        Die();
    }

    public bool isDead { get; private set; }

    void Die()
    {
        if (GetStat(StatType.Hp)<=0)
        {
            isDead = true;
        };

    }

}