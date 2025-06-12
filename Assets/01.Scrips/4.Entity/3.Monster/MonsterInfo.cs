using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//몬스터들이 가질 자신만의 정보들을 저장하는 클래스
public class MonsterInfo : MonoBehaviour
{
    public BaseState[] states;

    public Animator anim;
    public MonsterDataSo data;

    public string name;

    public int maxHp;
    public int currentHp;

    public int def;
    public int dodge;

    private void Awake()
    {
        name        =   data.name;
        maxHp       =   data.MaxHp;
        currentHp   =   maxHp;
        def         =   data.Def;
        dodge       =   data.Dodge;
    }

}
