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

    public int[] skillNumbers; //이걸 통해 원하는 스킬에 해당하는 인덱스값을 넣어줌 1번스킬 2번스킬 3번스킬 -> [1,2,3]
    public int actionNum; //원하는 스킬을 찾기위한(인덱스값) 변수

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
