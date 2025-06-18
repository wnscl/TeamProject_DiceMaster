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
            case MonsterState.Normal:   //Ȧ
                actionNumber = UnityEngine.Random.Range(0, 2);  // 0 1 �Ϲݽ�ų �� �� �ϳ�
                return actionNumber;

            case MonsterState.Happy:    //Ȧ
                actionNumber = UnityEngine.Random.Range(0, 3);  // 0 1 2 ���� ���� ��ų
                return actionNumber;

            case MonsterState.Sad:      //¦
                actionNumber = 0;                               // 0 �⺻��ų
                return actionNumber;

            case MonsterState.Angry:    //¦
                actionNumber = 2;                               // 2 �ݵ�� Ư����ų
                return actionNumber;

            default:
                actionNumber = UnityEngine.Random.Range(0, 3); //������, ����ó��
                return actionNumber;
        }
    }
    private void ConditionOfAttacker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //���ִ� �� ī��Ʈ ¦�� Ȧ�� �������� ��� 2�����ؼ� ���¸� �ٲ� �� �ְ�
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
        //�Ʊ��� �� ��ų �� �ִ� ������ ���� ���� �ؾ���
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
        //�Ʊ��� ������ �� ������ ���� �ɾ����
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
        //�÷��̾ ���������� 
        BattleModel bm = BattleManager.Instance.Battle.Model;
        if ((bm.turnCount % 2) == 0)
        {

        }
        else
        {

        }
    }

}
