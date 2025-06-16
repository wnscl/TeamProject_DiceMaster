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

//몬스터들이 가질 자신만의 정보들을 저장하는 클래스
public class MonsterInfo : EntityInfo
{

    public MonsterType      mobType;
    public MonsterState     mobState = MonsterState.Normal; //몬스터의 감정 상태 - 감정에 따라 사용하는 스킬 프리팹이 다르게 

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
