using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonster
{
    void ChangeState(MonsterState nextState);
    void StartState();
}
public enum MonsterState
{
    Idle,
    Decide,
    Action
}

public class Monster : MonoBehaviour, IMonster
{
    MonsterFsm fsm;
    MonsterInfo monsterInfo;

    private void Awake()
    {
        monsterInfo.states[0] = new Idle(monsterInfo);
        monsterInfo.states[1] = new Decide(monsterInfo);
        monsterInfo.states[2] = new Action(monsterInfo);

        fsm.InitFsm(monsterInfo.states[(int)MonsterState.Idle]);
    }


    public void ChangeState(MonsterState nextState)
    {
        fsm.ChangeState(monsterInfo.states[(int)nextState]);
    }
    public void StartState()
    {
        fsm.ActionState();
    }


}
