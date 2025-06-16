using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : EntityInfo
{
StatHandler Gm = GameManager.Instance.player.statHandler;
   

 public string name;

    public float maxHp;
    public float currentHp;

    public float def;
    public float mDef;
    public float dodge;

    public int actionNum; //?–´?–¤ ?Š¤?‚¬ ?‚¬?š©?• ì§? ê²°ì •

    void Awake()
    {
    }

    public float exp;

    public void GetExp()
    {
        exp += exp; //ëª¬ìŠ¤?„°ê°? ê°?ì§? expê°??
        
    }

    public void ResetExp()
    {
        exp = 0;
    }

   public void SetInfo()
    {
        name = Gm.statData.characterName;
        maxHp = Gm.GetStat(StatType.MaxHp);
        currentHp = Gm.GetStat(StatType.Hp);
        def = Gm.GetStat(StatType.PhysicalDefense);
        mDef = Gm.GetStat(StatType.MagicalDefense);
        dodge = Gm.GetStat(StatType.Evasion);

    }
    
    
}
