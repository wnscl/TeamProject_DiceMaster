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
    Idle,   //전투 중 사용될 아이들 상태
    Spawn,  //초기 전투화면으로 가고 한번 사용될 소환 상태
    Decide, //턴 준비 시 사용될 어떤 스킬 사용할지 결정하는 상태
    Action, //공격 버프 등 스킬 사용시 사용될 상태
    Dead    //죽으면 사용될 상태
}

public class Monster : MonoBehaviour, IMonster
{
    MonsterFsm fsm;
    MonsterInfo monsterInfo;

    private void Awake()
    {
        fsm = new MonsterFsm();

        monsterInfo.states[0] = new Idle(monsterInfo);
        monsterInfo.states[1] = new Spawn(monsterInfo); 
        monsterInfo.states[2] = new Decide(monsterInfo);
        monsterInfo.states[3] = new Action(monsterInfo);
        monsterInfo.states[4] = new Dead(monsterInfo);

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
