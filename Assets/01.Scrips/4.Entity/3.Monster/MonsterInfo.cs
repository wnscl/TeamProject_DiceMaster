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

    public SpriteRenderer feelSprite;
    public Sprite[] feelIcon;
    protected override void Awake()
    {
        name        =   data.name;
        maxHp       =   data.MaxHp;
        currentHp   =   maxHp;
        def         =   data.Def;
        dodge       =   data.Dodge;
        magicDef    =   data.MagicDef;

        mobType     =   data.MonsterType;
        buffList =  new List<IBuff>();
    }

}
