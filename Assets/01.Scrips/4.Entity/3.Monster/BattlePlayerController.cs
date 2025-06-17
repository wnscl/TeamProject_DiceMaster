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

    private IEnumerator DecideAction() //상태에 따라 어떤 행동을 할지 결정
    {
        playerInfo.actionNum = UnityEngine.Random.Range(0, 3); //actionNum을 지정해 원하는 스킬을 사용
        //actionNum은 skillNumbers의 인덱스값
        yield break;
    }
    private IEnumerator DoAction() //결정된 행동을 실행
    {
        yield return SkillManager.instance.skills[playerInfo.skillNumbers[playerInfo.actionNum]].OnUse();
        yield break;
    }
    private IEnumerator GetResult() //몬스터는 버프 디버프에 따른 계산 후 자신의 상태를 바꿈
    {
        yield return BuffManager.instance.UseBuff(this);
        yield break;
    }


    public void GetDamage(int dmg)
    {
        int chance = UnityEngine.Random.Range(0, 100);  //99퍼까지

        if (playerInfo.dodge > chance) return; //회피

        dmg = Mathf.Abs(dmg); //데미지는 절대값으로 

        playerInfo.currentHp =
            Mathf.Clamp(playerInfo.currentHp - dmg, 0, playerInfo.maxHp);

    }

    public EntityInfo GetEntityInfo()
    {
        return playerInfo;
    }
}
