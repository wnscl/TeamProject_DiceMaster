using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityInfo : MonoBehaviour
{
    public Animator anim;
    public MonsterDataSo data;

    public string name;

    public int maxHp;
    public int currentHp;

    public int def;
    public int dodge;
    public int magicDef;

    public int actionNum; //� ��ų ������� ����

    protected virtual void Awake()
    {
        name = data.name;
        maxHp = data.MaxHp;
        currentHp = maxHp;
        def = data.Def;
        dodge = data.Dodge;
        magicDef = data.MagicDef;
    }
    
}
