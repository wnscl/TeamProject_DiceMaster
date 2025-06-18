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
    private bool SelectSkill()
    {
        if (Input.GetKeyDown("1"))
        {
            playerInfo.actionNum = 0;
            return true;
        }
        if (Input.GetKeyDown("2"))
        {
            playerInfo.actionNum = 1;
            return true;
        }
        if (Input.GetKeyDown("3"))
        {
            playerInfo.actionNum = 2;
            return true;
        }
        if (Input.GetKeyDown("4"))
        {
            playerInfo.actionNum = 3;
            return true;
        }
        if (Input.GetKeyDown("5"))
        {
            playerInfo.actionNum = 4;
            return true;
        }
        if (Input.GetKeyDown("6"))
        {
            playerInfo.actionNum = 5;
            return true;
        }

        return false;
    }
    private IEnumerator DecideAction() //���¿� ���� � �ൿ�� ���� ����
    {
        bool isSelect = false;

        Debug.Log(isSelect);

        while (!isSelect)
        {
            isSelect = SelectSkill();
            if (playerInfo.skillNumbers[playerInfo.actionNum] == 999) isSelect = false;

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
        int chance = UnityEngine.Random.Range(0, 100); //의도된 99까지의 값

        if (playerInfo.dodge > chance)
        {
            UIManager.Instance.SystemMessage("회 ~ 피!", 0.8f);
            AudioManager.Instance.PlayAudioOnce(ReactSFXEnum.Evade);
            return;
        }

        float decrease = (float)playerInfo.def / ((float)playerInfo.def + 100f);
        dmg = (int)((float)dmg * (1 - decrease));

        playerInfo.currentHp =
            Mathf.Clamp(playerInfo.currentHp - dmg, 0, playerInfo.maxHp);

        if (playerInfo.currentHp <= 0)
        {
            playerInfo.anim.SetBool("isAction", true);
            playerInfo.anim.SetTrigger("Dead");
            //자신이 죽었다고 알려야함
            BattleManager.Instance.Battle.CheckBattleEnd(playerInfo);
        }

        playerInfo.currentHp =
            Mathf.Clamp(playerInfo.currentHp - dmg, 0, playerInfo.maxHp);

        AudioManager.Instance.PlayAudioOnce(ReactSFXEnum.Hit);
        UIManager.Instance.battleWindow.SetHPBar();
    }

    public EntityInfo GetEntityInfo()
    {
        return playerInfo;
    }
}
