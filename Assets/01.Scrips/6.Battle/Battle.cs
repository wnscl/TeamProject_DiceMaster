using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
3-2. 계산 : 주사위 결과값, 크리티컬, 회피 확률 등의 난수 조정을 실시한다.
3-3. 동작 : 조정된 난수를 토대로 전투를 실시한다. 기본적으로 턴을 가진 자의 행동을 우선하지만, 관련 스킬이나 특수 스테이터스가 있다면 선공권을 빼앗길 수도 있다.
3-4. 전환 : 각 유닛의 생존 여부 및 동작 단계에서의 버프, 디버프에 따른 처리를 시행하고 ( 독, 출혈 등 ) 턴을 전환한다.

4. 각 배틀 단계별 상세 내용
4-1. 조우 : 각 유닛별 스테이터스를 읽어와 배틀을 준비한다. 배틀 돌입 전에 적용된 아이템이나 버프가 있다면 이 단계에서 적용된다 ( 단, 스테이터스 증가와 같은 정보는 이미 플레이어 관련 스크립트에서 증가된 수치를 가지고 있을 것이니 기본적으로는 버프만 적용된다고 생각하면 될 것이다. 예를 들어 "배틀 돌입 시, 드랍률 증가" 와 같은 효과가 이 단계에서 적용된다 )
4-2. 전투 : 2, 3 의 내용 참조
4-3. 결과 : 승리, 패배에 따른 결과를 처리한다. 스토리 이벤트를 진행하거나 분기의 전환, 아이템 획득 등의 처리를 실행한다.
 */

public interface IBattleable
{
    bool IsDead { get; }
    int CurrentHp { get; set; }
}

public class Battle : MonoBehaviour
{
    //private Monster enemy;
    private bool BattleResult;

    private void Awake()
    {
        BattleManager.Instance.Battle = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 배틀 시작 전, 조우 단계에서 배틀을 준비하는 메소드
    /// </summary>
    public void Encounter()
    {
        BattleManager.Instance.IsBattleActive = true;


    }

    /// <summary>
    /// 배틀 중, 실제로 전투를 실행하는 부분의 메소드
    /// </summary>
    public void Combat()
    {

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
        // to do : 플레이어와 적의 체력을 체크하고, 어느 쪽의 체력이 0 이하가 되었는지 체크
        // 플레이어가 체력이 0이 되었을 경우, 게임 오버 처리 혹은 배틀 패배 처리
        // 모든 적의 체력이 0이 되었을 경우, 배틀 승리 처리
        // 플레이어와 적 중 체력이 남은 유닛이 있을 경우, 턴 전환

        if (BattleManager.Instance.Player.IsDead)
        {
            // to do : 패배 처리, 혹은 게임 오버 처리
            BattleResult = false;

            EndBattle();
        }
        else if (BattleManager.Instance.Enemies.All(e => e.IsDead))
        {
            // to do : 승리 처리, 아이템 드랍, 경험치 획득 등
            BattleResult = true;

            EndBattle();
        }
        else
        {
            TurnShift();
        }
    }
}
