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

    public int[] skillNumbers; //�̰� ���� ���ϴ� ��ų�� �ش��ϴ� �ε������� �־��� 1����ų 2����ų 3����ų -> [1,2,3]
    public int actionNum; //���ϴ� ��ų�� ã������(�ε�����) ����

    public List<IBuff> buffList;

    protected virtual void Awake()
    {
        name = data.name;
        maxHp = data.MaxHp;
        currentHp = maxHp;
        def = data.Def;
        dodge = data.Dodge;
        magicDef = data.MagicDef;
        
        buffList = new List<IBuff> ();
    }
    
}
