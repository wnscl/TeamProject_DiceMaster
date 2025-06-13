using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;



public class MonsterController : MonoBehaviour, IBattleEntity
{
    [SerializeField] private MonsterInfo monsterInfo;

    private Dictionary<BattlePhase, Func<IEnumerator>> _fsm;
    //이러한 방식은 func를 통해 델리게이트 a  a += 메서드 처럼 멀티캐스트를
    //위한 델리게이트를 만드는 것이 아니라
    //하나의 함수에 대응하는 델리게이트를 만든다.
    //즉 _fsm.add(키값, 메서드한개)에 해당하는 델리게이트를
    //딕셔너리에 쌓아두는 것
   
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
        //지금 딕셔너리는 3개의 값이 있는 것이다.
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

    private IEnumerator DecideAction() //어떤 행동할지 결정
    {
        
        yield break;
    }
    private IEnumerator DoAction() //결정된 행동을 실행
    {
        monsterInfo.anim.SetBool("isAction", true);

        yield break;
    }
    private IEnumerator GetResult() //버프 디버프를 받음
    {

        yield break;
    }

    public void GetDamage(int dmg)
    {
        int chance = UnityEngine.Random.Range(0, 100);  //99퍼까지

        if (monsterInfo.dodge > chance) return; //회피

        dmg = Mathf.Abs(dmg); //데미지는 절대값으로 

        monsterInfo.currentHp = 
            Mathf.Clamp(monsterInfo.currentHp - dmg, 0, monsterInfo.maxHp);

    }


}
