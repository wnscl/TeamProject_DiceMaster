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
        //���� �÷��̾��� ü���� ���ٸ�
        //���� �÷��̾ ��� ��ų�� ����� �Ѵٸ�
        //���� �� ü���� �÷��̾�� ���ٸ�


        
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
