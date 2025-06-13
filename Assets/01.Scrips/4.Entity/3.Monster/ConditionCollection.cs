using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterType
{
    Attacker,
    Tanker,
    Supporter,
    Bruiser,
    Allrounder
}

public class ConditionCollection : MonoBehaviour
{
    public ConditionCollection instance;


    private void Awake()
    {
        instance = this;

        conditionBox = new Dictionary<MonsterType, Func<MonsterInfo, PlayerInfo, int>>
        {
            {MonsterType.Attacker, ConditionOfAttacker },
            {MonsterType.Tanker, ConditionOfTanker },
            {MonsterType.Supporter, ConditionOfSupporter },
            {MonsterType.Bruiser, ConditionOfBruiser },
            {MonsterType.Allrounder, ConditionOfAllrounder }
        };
    }



    private Dictionary<MonsterType, Func<MonsterInfo, PlayerInfo, int>> conditionBox;
    //PlayerInfo
    [SerializeField] private MonsterInfo monsterInfo;
    [SerializeField] private PlayerInfo playerInfo; 

    //[SerializeField] private Pl

    public void GetRequester(MonsterInfo info)
    {
        monsterInfo = info;
    }
    public int GetCondition(MonsterType type)
    {
        return conditionBox[type].Invoke(monsterInfo, playerInfo);
    }

    private int ConditionOfAttacker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //만약 플레이어의 체력이 낮다면
        //만약 플레이어가 어떠한 스킬을 사용을 한다면
        //만약 내 체력이 플레이어보다 많다면


        
        return 0;
    }
    private int ConditionOfTanker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        return 0;
    }
    private int ConditionOfSupporter(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        return 0;
    }
    private int ConditionOfBruiser(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        return 0;
    }
    private int ConditionOfAllrounder(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        return 0;
    }





}
