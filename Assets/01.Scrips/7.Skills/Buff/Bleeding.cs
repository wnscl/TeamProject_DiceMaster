using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBuff
{
    bool Execute(IBattleEntity entity);
    IBuff Clone();
}

public class Bleeding : IBuff
{
    private int buffCount;
    private int damage;

    public Bleeding()
    {
        buffCount = 2;
        damage = 4;
    }

    public bool Execute(IBattleEntity entity)
    {
        if(buffCount == 0) return false;
        buffCount--;
        EntityInfo info = entity.GetEntityInfo();

        info.currentHp = Mathf.Clamp(info.currentHp - damage, 0, info.maxHp);
        return true;
    }
    public IBuff Clone()
    {
        return new Bleeding();
    }
}
