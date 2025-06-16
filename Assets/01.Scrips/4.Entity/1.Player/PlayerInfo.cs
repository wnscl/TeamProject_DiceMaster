using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerInfo : EntityInfo
{
    public StatHandler Gm;
   

    public int exp;

    public void GetExp()
    {
        exp += exp; 
       
    }

    public void ResetExp()
    {
        exp = 0;
    }

    protected override void Awake()
    {
        SetInfo();
    }


    public void SetInfo()
     {
        name = data.name;
        maxHp = Gm.GetStat(StatType.MaxHp);
        currentHp = Gm.GetStat(StatType.Hp);
        def = Gm.GetStat(StatType.PhysicalDefense);
        magicDef = Gm.GetStat(StatType.MagicalDefense);
        dodge = Gm.GetStat(StatType.Evasion);

    }
    
    
    
    
    // 테스트용 매서드 자리 나중에 지우면 됨
[Button]
    public void TestMinusHP()//테스트용 매서드
    {
     
        UIManager.Instance.battleWindow.SetHPBar();
        
    }
[Button]
    public void TestPlusHP()
    {
        
        UIManager.Instance.battleWindow.SetHPBar();
    }

    


}
