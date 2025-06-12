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
    Idle,   //���� �� ���� ���̵� ����
    Spawn,  //�ʱ� ����ȭ������ ���� �ѹ� ���� ��ȯ ����
    Decide, //�� �غ� �� ���� � ��ų ������� �����ϴ� ����
    Action, //���� ���� �� ��ų ���� ���� ����
    Dead    //������ ���� ����
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
