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
                actionNumber = UnityEngine.Random.Range(0, 2); // 0 1 �Ϲݽ�ų �� �� �ϳ�
                return actionNumber;

            case MonsterState.Happy:
                actionNumber = UnityEngine.Random.Range(0, 3); // 0 1 2 ���� ���� ��ų
                return actionNumber;

            case MonsterState.Angry:
                actionNumber = UnityEngine.Random.Range(1, 3); // 2 �ݵ�� Ư����ų
                return actionNumber;

            case MonsterState.Sad:
                actionNumber = UnityEngine.Random.Range(0, 1); // 0 �⺻��ų
                return actionNumber;

            default:
                actionNumber = UnityEngine.Random.Range(0, 3); //������
                return actionNumber;
        }
    }
    private int ConditionOfAttacker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //�� �� �������� ������ ���̾��� �ؾ���
        return GetActionNumber(mobInfo.mobState);
    }

    private int ConditionOfTanker(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //�Ʊ��� �� ��ų �� �ִ� ������ ���� ���� �ؾ���
        return GetActionNumber(mobInfo.mobState);
    }

    private int ConditionOfSupporter(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //�Ʊ��� ������ �� ������ ���� �ɾ����
        return GetActionNumber(mobInfo.mobState);
    }

    private int ConditionOfAllrounder(MonsterInfo mobInfo, PlayerInfo playerInfo)
    {
        //�÷��̾ ���������� 
        return GetActionNumber(mobInfo.mobState);
    }





}
