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
public class MonsterInfo : MonoBehaviour
{

    public Animator         anim;
    public MonsterDataSo    data;

    public string           name;

    public int              maxHp;
    public int              currentHp;

    public int              def;
    public int              dodge;
    public int              magicDef;

    public MonsterType      mobType;
    public MonsterState     mobState = MonsterState.Normal; //������ ���� ���� - ������ ���� ����ϴ� ��ų �������� �ٸ��� 
    public int              actionNum; //� ��ų ������� ����

    private void Awake()
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
