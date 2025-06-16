using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BattlePlayerController : MonoBehaviour, IBattleEntity
{

    private Dictionary<BattlePhase, Func<IEnumerator>> _fsm;

    [SerializeField] private PlayerInfo playerInfo;

    public Coroutine playerCor;

    public bool isSelectAction = false;

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
        isSelectAction = true;

        playerInfo.actionNum = 0;
    }


    public IEnumerator ActionOnTurn(BattlePhase nowTurn)
    {
        yield return _fsm[nowTurn];
        yield break;
    }

    private IEnumerator DecideAction() //���¿� ���� � �ൿ�� ���� ����
    {
        while (!isSelectAction)
        {
            yield return null;
        }
        yield break;
    }
    private IEnumerator DoAction() //������ �ൿ�� ����
    {
        isSelectAction = false;
        playerInfo.anim.SetBool("isAction", true);

        //���� ���� ��ų �������� 3�� 
        Instantiate(
            playerInfo.data._SkillPrefabs[playerInfo.actionNum],
            transform.position, Quaternion.identity, this.transform);

        yield break;
    }
    private IEnumerator GetResult() //���ʹ� ���� ������� ���� ��� �� �ڽ��� ���¸� �ٲ�
    {

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
