using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Normal,
    Happy,
    Sad,
    Angry
}

//���͵��� ���� �ڽŸ��� �������� �����ϴ� Ŭ����
public class MonsterInfo : EntityInfo
{

    public MonsterType      mobType;
    public MonsterState     mobState = MonsterState.Normal; //������ ���� ���� - ������ ���� ����ϴ� ��ų �������� �ٸ��� 

    protected override void Awake()
    {
        name        =   data.name;
        maxHp       =   data.MaxHp;
        currentHp   =   maxHp;
        def         =   data.Def;
        dodge       =   data.Dodge;
        magicDef    =   data.MagicDef;

        mobType     =   data.MonsterType;
    }

}
