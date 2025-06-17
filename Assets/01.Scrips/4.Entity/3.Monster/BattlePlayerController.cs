using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BattlePlayerController : MonoBehaviour, IBattleEntity
{

    [SerializeField] private PlayerInfo playerInfo;

    public Coroutine playerCor;

    public bool isSelectAction = false;



    [Button]
    private void ActionTest()
    {
        isSelectAction = true;

        //playerInfo.actionNum = 0;
    }


    public IEnumerator ActionOnTurn(BattlePhase nowTurn)
    {
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
        playerInfo.actionNum = UnityEngine.Random.Range(0, 3); //actionNum�� ������ ���ϴ� ��ų�� ���
        //actionNum�� skillNumbers�� �ε�����
        yield break;
    }
    private IEnumerator DoAction() //������ �ൿ�� ����
    {
        yield return SkillManager.instance.skills[playerInfo.skillNumbers[playerInfo.actionNum]].OnUse();
        yield break;
    }
    private IEnumerator GetResult() //���ʹ� ���� ������� ���� ��� �� �ڽ��� ���¸� �ٲ�
    {
        yield return BuffManager.instance.UseBuff(this);
        yield break;
    }


    public void GetDamage(int dmg)
    {
        int chance = UnityEngine.Random.Range(0, 100);  //99�۱���

        if (playerInfo.dodge > chance) return; //ȸ��

        dmg = Mathf.Abs(dmg); //�������� ���밪���� 

        playerInfo.currentHp =
            Mathf.Clamp(playerInfo.currentHp - dmg, 0, playerInfo.maxHp);

    }

    public EntityInfo GetEntityInfo()
    {
        return playerInfo;
    }
}
