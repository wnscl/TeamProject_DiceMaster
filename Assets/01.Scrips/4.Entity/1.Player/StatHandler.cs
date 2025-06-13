using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StatHandler : MonoBehaviour,IBattleEntity
{
    public StatData statData;
    private Dictionary<StatType, float> currentStats = new Dictionary<StatType, float>();

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