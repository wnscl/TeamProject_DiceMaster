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

    }

    private IEnumerator DecideAction() //���¿� ���� � �ൿ�� ���� ����
    {
        bool isSelect = false;

        Debug.Log(isSelect);

        while (!isSelect)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("입력감지되었음");
                playerInfo.actionNum = 0;
                isSelect = true;
            }
            yield return null;
        }

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

        if (playerInfo.dodge > chance)
        {
            AudioManager.Instance.PlayAudioOnce(ReactSFXEnum.Evade);
            return;
        } //ȸ��

        dmg = Mathf.Abs(dmg); //�������� ���밪���� 

        playerInfo.currentHp =
            Mathf.Clamp(playerInfo.currentHp - dmg, 0, playerInfo.maxHp);
        AudioManager.Instance.PlayAudioOnce(ReactSFXEnum.Hit);
    }

    public EntityInfo GetEntityInfo()
    {
        return playerInfo;
    }
}
