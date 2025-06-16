using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

/*
1. 전투는 [ 조우 ], [ 전투 ], [ 결과 ] 의 3단계로 나뉘어진다.
1-1. 전투 중의 이벤트를 상정, BattleEvent는 조우, 전투, 결과 어느 곳이던 나타날 수 있다

2. [ 전투 ] 단계의 페이즈 시프트
→ 배틀 시스템은 기본적으로 <슈퍼로봇대전> 의 시스템을 레퍼런스로 설계한다
2-1. 준비 : 이번 턴에 무엇을 할 것인지 준비하는 단계.
2-2. 계산 : 각자의 동작에 따른 난수가 있다면 이에 따른 처리를 한다.
2-2. 동작 : [ 준비 ] 단계에서 설정한 행동을 한다. 턴을 가진 자의 행동을 우선한다.
2-3. 전환 : [ 동작 ] 단계의 결과를 확인하고 턴을 전환한다.

3. 각 페이즈 상세 내용
3-1. 준비 : 자신의 행동을 설정한다. 행동은 크게 공격, 방어, 회피, 아이템 사용, 기획에 부합한다면 도주 또한 행동에 포함된다.
3-2. 계산 ( 준비 단계에서 실행 ) : 주사위 결과값, 크리티컬, 회피 확률 등의 난수 조정을 실시한다.
3-3. 동작 : 조정된 난수를 토대로 전투를 실시한다. 기본적으로 턴을 가진 자의 행동을 우선하지만, 관련 스킬이나 특수 스테이터스가 있다면 선공권을 빼앗길 수도 있다.
3-4. 전환 : 각 유닛의 생존 여부 및 동작 단계에서의 버프, 디버프에 따른 처리를 시행하고 ( 독, 출혈 등 ) 턴을 전환한다.

4. 각 배틀 단계별 상세 내용
4-1. 조우 : 각 유닛별 스테이터스를 읽어와 배틀을 준비한다. 배틀 돌입 전에 적용된 아이템이나 버프가 있다면 이 단계에서 적용된다 ( 단, 스테이터스 증가와 같은 정보는 이미 플레이어 관련 스크립트에서 증가된 수치를 가지고 있을 것이니 기본적으로는 버프만 적용된다고 생각하면 될 것이다. 예를 들어 "배틀 돌입 시, 드랍률 증가" 와 같은 효과가 이 단계에서 적용된다 )
4-2. 전투 : 2, 3 의 내용 참조
4-3. 결과 : 승리, 패배에 따른 결과를 처리한다. 스토리 이벤트를 진행하거나 분기의 전환, 아이템 획득 등의 처리를 실행한다.
 */


public class Battle : MonoBehaviour
{

    private BattleModel model;


    ////private Monster enemy;
    //private bool BattleResult;
    //private List<IBattleEntity> battleEntities; // 전투에 참가하는 유닛 컬렉션
    //public IBattleEntity nowTurnEntity;


    //public IBattleEntity player;
    //public IBattleEntity monster; //테스트를 위한 몬스터와 플레이어 참조 필드


    //private BattlePhase battlePhase;
    //private int turnCount; // 현재 턴 수

    //private void Awake()
    //{
    //    BattleManager.Instance.Battle = this;
    //}

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 전투에 참여할 적 몬스터를 가져오는 메소드
    /// </summary>
    public void GetEmenies()
    {
        // to do : 게임 사양 상 추후 리스트로 받아와야 할 것이다
        IBattleEntity enemies = GameManager.Instance.monster;

        model.battleEntities.Add(enemies);
    }

    public void GetPlayer()
    {
        IBattleEntity player = model.player;

        model.battleEntities.Add(player);
    }

    /// <summary>
    /// 배틀 시작 전, 조우 단계에서 배틀을 준비하는 메소드
    /// </summary>
    [Button]
    public void Encounter()
    {
        // 전투 관련 필드 값 전환
        BattleManager.Instance.IsBattleActive = true;
        model.battlePhase = BattlePhase.Ready;

        StartCoroutine(Combat());
    }

    /// <summary>
    /// 배틀 중, 실제로 전투를 실행하는 부분의 메소드
    /// </summary>
    public IEnumerator Combat()
    {
        // 전투 참여 중인 각 유닛의 배틀 상태를 활성화
        while (BattleManager.Instance.IsBattleActive == true)
        {
            switch (model.battlePhase)
            {
                case BattlePhase.Ready:
                    // 각 유닛의 행동을 설정
                    foreach (IBattleEntity entity in model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // 현재 턴을 가진 유닛 설정

                        yield return entity.ActionOnTurn(model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Action;
                    break;
                case BattlePhase.Action:
                    // 설정한 행동 실행
                    foreach (IBattleEntity entity in model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // 현재 턴을 가진 유닛 설정

                        yield return entity.ActionOnTurn(model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Result;
                    break;
                case BattlePhase.Result:
                    // 턴 종료 시의 처리
                    foreach (IBattleEntity entity in model.battleEntities)
                    {
                        yield return entity.ActionOnTurn(model.battlePhase);
                    }

                    // 각 유닛의 행동 결과를 처리하고, 배틀 종료 여부를 확인
                    CheckBattleEnd();
                    break;
            }
        }
    }

    /// <summary>
    /// 배틀 종료 후, 결과를 처리하는 메소드
    /// </summary>
    public void EndBattle()
    {
        BattleManager.Instance.IsBattleActive = false;
    }

    /// <summary>
    /// 유닛의 행동을 설정하는 메소드
    /// </summary>
    /// <param name="unit"></param>
    public void SetAction()
    {

    }

    /// <summary>
    /// 각 유닛이 선택한 행동에 대한 사전 처리를 하는 메소드
    /// </summary>
    public void ProcessAction()
    {

    }

    /// <summary>
    /// 유닛의 행동을 실행하는 메소드
    /// </summary>
    public void ExcuteAction()
    {

    }

    /// <summary>
    /// 공격 턴을 전환하는 메소드
    /// 기존에 턴 플래그를 가지고 있는 유닛의 턴 플래그를 해제하고, 턴 플래그가 없는 유닛의 턴 플래그를 설정한다
    /// </summary>
    public void TurnShift()
    {
        
    }

    /// <summary>
    /// 배틀 결과를 확인하는 메소드
    /// </summary>
    /// <returns></returns>
    private void CheckBattleEnd()
    {
        // to do : 리스트에 플레이어가 없다면 게임 오버시키는 감지 기능을 고려해두자
        // 배틀에 참여한 유닛 리스트에 플레이어만 남아있을 경우, 배틀을 플레이어의 승리로 처리하고 배틀을 종료한다

        if (model.battleEntities.Count == 0 || model.battleEntities.All(entity => entity is Player))
        {
            model.BattleResult = true; // 플레이어 승리
            EndBattle();
        }
        else
        {
            // 배틀이 계속 진행 중인 경우, 다음 턴으로 넘어간다
            model.battlePhase = BattlePhase.Ready;
            model.turnCount++;
        }
    }
}
