using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���͵��� ���� �ڽŸ��� �������� �����ϴ� Ŭ����
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
