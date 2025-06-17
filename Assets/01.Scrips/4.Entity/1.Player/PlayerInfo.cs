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
        SetData();
    }

    public void SetData()
    {
        name = data.name;
        maxHp = data.MaxHp;
        currentHp = maxHp;
        def = data.Def;
        magicDef = data.MagicDef;
        dodge = data.Dodge;

    }
    public void SetInfo()
    {
        name = data.name;
        maxHp = Gm.GetStat(StatType.MaxHp);
        currentHp = Gm.GetStat(StatType.Hp);
        def = Gm.GetStat(StatType.PhysicalDefense);
        magicDef = Gm.GetStat(StatType.MagicalDefense);
        dodge = Gm.GetStat(StatType.Evasion);

        buffList = new List<IBuff>();
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
