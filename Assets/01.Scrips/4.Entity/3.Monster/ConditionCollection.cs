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

        conditionBox = new Dictionary<MonsterType, Action<MonsterInfo, PlayerInfo>>
        {
            {MonsterType.Attacker, ConditionOfAttacker },
            {MonsterType.Tanker, ConditionOfTanker },
            {MonsterType.Supporter, ConditionOfSupporter },
            {MonsterType.Allrounder, ConditionOfAllrounder }
        };
    }

    private Dictionary<MonsterType, Action<MonsterInfo, PlayerInfo>> conditionBox;
    //PlayerInfo
    [SerializeField] private MonsterInfo monsterInfo;
    [SerializeField] private PlayerInfo playerInfo; 

    public void GetMonster(MonsterInfo info)
    {
        monsterInfo = info;
    }
    public void GetCondition(MonsterType type)
    {
        conditionBox[type].Invoke(monsterInfo, playerInfo);
    }
    public int GetActionNumber(MonsterState state)
    {
        int actionNumber = 0; 
        switch (monsterInfo.mobState)
        {
            case MonsterState.Normal:   //홀
                actionNumber = UnityEngine.Random.Range(0, 2);  // 0 1 일반스킬 둘 중 하나
                return actionNumber;

            case MonsterState.Happy:    //홀
                actionNumber = UnityEngine.Random.Range(0, 3);  // 0 1 2 완전 랜덤 스킬
                return actionNumber;

            case MonsterState.Sad:      //짝
                actionNumber = 0;                               // 0 기본스킬
                return actionNumber;

            case MonsterState.Angry:    //짝
                actionNumber = 2;                               // 2 반드시 특수스킬
                return actionNumber;

            default:
                actionNumber = UnityEngine.Random.Range(0, 3); //방어로직, 예외처리
                return actionNumber;
        }
    }
    private void ConditionOfAttacker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //좀있다 턴 카운트 짝수 홀수 기준으로 기분 2개씩해서 상태를 바꿀 수 있게
        BattleModel bm = BattleManager.Instance.Battle.Model;
        if ((bm.turnCount % 2) == 0)
        {

        }
        else
        {

        }

        if (playerInfo.currentHp > monsterInfo.currentHp) mobInfo.mobState = MonsterState.Angry;
        else mobInfo.mobState = MonsterState.Happy;

        if (mobInfo.buffList != null && mobInfo.buffList.Count > 5) mobInfo.mobState = MonsterState.Sad;

        if (mobInfo.buffList == null && playerInfo.currentHp > (playerInfo.maxHp / 3)) mobInfo.mobState = MonsterState.Normal;

        mobInfo.feelSprite.sprite = mobInfo.feelIcon[(int)mobInfo.mobState];
    }

    private void ConditionOfTanker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //아군을 잘 지킬 수 있는 패턴을 많이 쓰게 해야함
        BattleModel bm = BattleManager.Instance.Battle.Model;
        if ((bm.turnCount % 2) == 0)
        {

        }
        else
        {

        }
    }

    private void ConditionOfSupporter(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //아군이 위험할 때 버프를 많이 걸어야함
        BattleModel bm = BattleManager.Instance.Battle.Model;
        if ((bm.turnCount % 2) == 0)
        {

        }
        else
        {

        }
    }

    private void ConditionOfAllrounder(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //플레이어를 괴롭혀야함 
        BattleModel bm = BattleManager.Instance.Battle.Model;
        if ((bm.turnCount % 2) == 0)
        {

        }
        else
        {

        }
    }

}
