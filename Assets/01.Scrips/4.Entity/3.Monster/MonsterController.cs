using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;



public class MonsterController : MonoBehaviour, IBattleEntity
{
    [SerializeField] private MonsterInfo monsterInfo;

    private Dictionary<BattlePhase, Func<IEnumerator>> _fsm;
    //�̷��� ����� func�� ���� ��������Ʈ a  a += �޼��� ó�� ��Ƽĳ��Ʈ��
    //���� ��������Ʈ�� ����� ���� �ƴ϶�
    //�ϳ��� �Լ��� �����ϴ� ��������Ʈ�� �����.
    //�� _fsm.add(Ű��, �޼����Ѱ�)�� �ش��ϴ� ��������Ʈ��
    //��ųʸ��� �׾Ƶδ� ��
   
    public BattlePhase testTurn;

    public Coroutine mobCor;


    private void Awake()
    {
        _fsm = new Dictionary<BattlePhase, Func<IEnumerator>>
        {
            {BattlePhase.Ready, DecideAction },
            {BattlePhase.Action, DoAction },
            {BattlePhase.Result, GetResult }
        };
        //���� ��ųʸ��� 3���� ���� �ִ� ���̴�.
    }


    [Button]
    private void ActionTest()
    {
        if (mobCor != null) StopCoroutine(mobCor);

        mobCor = StartCoroutine(ActionOnTurn(testTurn));
    }

    public IEnumerator ActionOnTurn(BattlePhase nowTurn)
    {
        yield return _fsm[nowTurn];

        yield break;
    }

    private IEnumerator DecideAction() //� �ൿ���� ����
    {
        
        yield break;
    }
    private IEnumerator DoAction() //������ �ൿ�� ����
    {
        monsterInfo.anim.SetBool("isAction", true);

        yield break;
    }
    private IEnumerator GetResult() //���� ������� ����
    {

        yield break;
    }

    public void GetDamage(int dmg)
    {
        int chance = UnityEngine.Random.Range(0, 100);  //99�۱���

        if (monsterInfo.dodge > chance) return; //ȸ��

        dmg = Mathf.Abs(dmg); //�������� ���밪���� 

        monsterInfo.currentHp = 
            Mathf.Clamp(monsterInfo.currentHp - dmg, 0, monsterInfo.maxHp);

    }


}
