using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterType
{
    Attacker,
    Tanker,
    Supporter,
    Allrounder
}

public class ConditionCollection : MonoBehaviour
{
    public static ConditionCollection instance;


    private void Awake()
    {
        instance = this;

        conditionBox = new Dictionary<MonsterType, Func<MonsterInfo, PlayerInfo, int>>
        {
            {MonsterType.Attacker, ConditionOfAttacker },
            {MonsterType.Tanker, ConditionOfTanker },
            {MonsterType.Supporter, ConditionOfSupporter },
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
    private int GetActionNumber(MonsterState state)
    {
        int actionNumber = 0; 
        switch (monsterInfo.mobState)
        {
            case MonsterState.Normal:
                actionNumber = UnityEngine.Random.Range(0, 2); // 0 1 일반스킬 둘 중 하나
                return actionNumber;

            case MonsterState.Happy:
                actionNumber = UnityEngine.Random.Range(0, 3); // 0 1 2 완전 랜덤 스킬
                return actionNumber;

            case MonsterState.Angry:
                actionNumber = UnityEngine.Random.Range(1, 3); // 2 반드시 특수스킬
                return actionNumber;

            case MonsterState.Sad:
                actionNumber = UnityEngine.Random.Range(0, 1); // 0 기본스킬
                return actionNumber;

            default:
                actionNumber = UnityEngine.Random.Range(0, 3); //방어로직
                return actionNumber;
        }
    }
    private int ConditionOfAttacker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //좀 더 위협적인 패턴을 많이쓰게 해야함
        return GetActionNumber(mobInfo.mobState);
    }

    private int ConditionOfTanker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //아군을 잘 지킬 수 있는 패턴을 많이 쓰게 해야함
        return GetActionNumber(mobInfo.mobState);
    }

    private int ConditionOfSupporter(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //아군이 위험할 때 버프를 많이 걸어야함
        return GetActionNumber(mobInfo.mobState);
    }

    private int ConditionOfAllrounder(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //플레이어를 괴롭혀야함 
        return GetActionNumber(mobInfo.mobState);
    }





}
