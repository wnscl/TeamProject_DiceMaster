using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected MonsterInfo monsterInfo;

    protected BaseState(MonsterInfo info)
    {
        monsterInfo = info;
    }

    public abstract void InitState();
    public abstract void ActionState();

}
