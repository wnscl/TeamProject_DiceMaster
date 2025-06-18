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
        //yield return _fsm[nowTurn];

        switch (nowTurn)
        {
            case BattlePhase.Ready:
                yield return DecideAction();
                break;

            case BattlePhase.Action:
                yield return DoAction();
                break;

            case BattlePhase.Result:
                yield return GetResult();
                break;
        }
        yield break;
    }

    private IEnumerator DecideAction() //���¿� ���� � �ൿ�� ���� ����
    {
        monsterInfo.actionNum = ConditionCollection.instance.GetActionNumber(monsterInfo.mobState);
        yield break;
    }
    private IEnumerator DoAction() //������ �ൿ�� ����
    {
        if(monsterInfo.currentHp <= 0) yield break;

        yield return SkillManager.instance.skills[monsterInfo.skillNumbers[monsterInfo.actionNum]].OnUse();
        yield break;
    }
    private IEnumerator GetResult() //���ʹ� ���� ������� ���� ��� �� �ڽ��� ���¸� �ٲ�
    {
        yield return BuffManager.instance.UseBuff(this);
        ConditionCollection.instance.GetCondition(monsterInfo.mobType);
        yield break;
    }

    public void GetDamage(int dmg)
    {
        int chance = UnityEngine.Random.Range(0, 100);  //99�۱���

        if (monsterInfo.dodge > chance)
        {
            AudioManager.Instance.PlayAudioOnce(ReactSFXEnum.Evade);
            return;
        }

        float decrease = (float)monsterInfo.def / ((float)monsterInfo.def + 100f); 
        dmg = (int)((float)dmg * (1 - decrease));

        monsterInfo.currentHp = 
            Mathf.Clamp(monsterInfo.currentHp - dmg, 0, monsterInfo.maxHp);

        if (monsterInfo.currentHp <= 0)
        {
            monsterInfo.anim.SetBool("isAction", true);
            monsterInfo.anim.SetTrigger("Dead");
            BattleManager.Instance.Battle.CheckBattleEnd(monsterInfo);
        }

        AudioManager.Instance.PlayAudioOnce(ReactSFXEnum.Hit);
        UIManager.Instance.battleWindow.SetHPBar();
    }

    public EntityInfo GetEntityInfo()
    {
        return monsterInfo;
    }
}
//        if (monsterInfo.currentHp < (monsterInfo.maxHp / 3)) monsterInfo.mobState = MonsterState.Angry;
