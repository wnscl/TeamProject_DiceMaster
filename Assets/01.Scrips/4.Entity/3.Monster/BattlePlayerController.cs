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
        //지금 딕셔너리는 3개의 값이 있는 것이다.
    }


    [Button]
    private void ActionTest()
    {
        isSelectAction = true;

        //playerInfo.actionNum = 0;
    }


    public IEnumerator ActionOnTurn(BattlePhase nowTurn)
    {
        yield return _fsm[nowTurn];
        yield break;
    }

    private IEnumerator DecideAction() //상태에 따라 어떤 행동을 할지 결정
    {
        playerInfo.actionNum = UnityEngine.Random.Range(0, 3);
        yield break;
    }
    private IEnumerator DoAction() //결정된 행동을 실행
    {
        yield return SkillManager.instance.skills[playerInfo.skillNumbers[playerInfo.actionNum]].OnUse();
        yield break;
    }
    private IEnumerator GetResult() //몬스터는 버프 디버프에 따른 계산 후 자신의 상태를 바꿈
    {

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
