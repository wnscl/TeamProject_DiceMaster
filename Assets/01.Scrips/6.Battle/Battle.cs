﻿using System.Collections;
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

    [SerializeField] private BattleModel model;

    Coroutine myCor;

    public BattleModel Model => model;


    [Button]
    /// <summary>
    /// 전투에 참여할 플레이어를 가져오는 메소드
    /// </summary>
    public void GetPlayer()
    {
        IBattleEntity player = SkillManager.instance.TestPlayer.GetComponent<IBattleEntity>();


        if (player != null)
        {
            model.battleEntities.Add(player);

            // model.PlayerInfo = player.GetEntityInfo() as PlayerInfo;
        }
        else
        {
            Debug.LogWarning("플레이어 인스턴스 취득에 실패하였습니다.");
        }
    }

    [Button]
    /// <summary>
    /// 전투에 참여할 적 몬스터를 가져오는 메소드
    /// </summary>
    public void GetEmenies()
    {
        // to do : 게임 사양 상 추후 리스트로 받아와야 할 것이다
        IBattleEntity enemies = SkillManager.instance.TestMonster.GetComponent<IBattleEntity>();

        model.battleEntities.Add(enemies);
        // model.EnemyInfo = enemies.GetEntityInfo() as MonsterInfo;
    }

    /// <summary>
    /// 배틀 시작 전, 조우 단계에서 배틀을 준비하는 메소드
    /// </summary>
    [Button]
    public void Encounter()
    {
        // 너티 어트리뷰트 버튼을 통한 테스트용 battleEntities 초기화
        GetPlayer();
        GetEmenies();

        GameManager.Instance.ActionPlayer();

        // 전투 관련 필드 값 전환
        BattleManager.Instance.IsBattleActive = true;
        model.isTurn = true;
        model.battlePhase = BattlePhase.Ready;

        AudioManager.Instance.ChangeAudio(AudioManager.Instance.audioPool.battleAudio, 2);

        myCor = StartCoroutine(Combat());
    }

    /// <summary>
    /// 배틀 중, 실제로 전투를 실행하는 부분의 메소드
    /// </summary>
    public IEnumerator Combat()
    {
        // 전투 참여 중인 각 유닛의 배틀 상태를 활성화
        while (BattleManager.Instance.IsBattleActive == true || Model.isTurn)
        {
            switch (Model.battlePhase)
            {
                case BattlePhase.Ready:
                    // 각 유닛의 행동을 설정
                    foreach (IBattleEntity entity in Model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // 현재 턴을 가진 유닛 설정
                        yield return entity.ActionOnTurn(Model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Action;

                    Debug.Log("End Ready Phase");
                    break;
                case BattlePhase.Action:
                    // 설정한 행동 실행
                    foreach (IBattleEntity entity in Model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // 현재 턴을 가진 유닛 설정

                        yield return entity.ActionOnTurn(Model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Result;

                    Debug.Log("End Action Phase");
                    break;
                case BattlePhase.Result:
                    // 턴 종료 시의 처리

                    if (BattleManager.Instance.IsBattleActive)
                    {
                        foreach (IBattleEntity entity in Model.battleEntities)
                        {
                            yield return entity.ActionOnTurn(Model.battlePhase);
                        }
                    }
                    else
                    {
                        model.isTurn = false;
                        yield return new WaitForSeconds(1.5f);
                    }

                    // 각 유닛의 행동 결과를 처리하고, 배틀 종료 여부를 확인
                    RotateTurn();
                    Debug.Log("End Result Phase");
                    break;
            }
        }
    }

    public void CheckBattleEnd(EntityInfo info)
    {
        Model.BattleResult = false;

        if (info.name == "BattlePlayer") Model.BattleResult = true;

        BattleManager.Instance.IsBattleActive = false;
    }
    private void RotateTurn()
    {
        if (!BattleManager.Instance.IsBattleActive)
        {
            StopBattle();
            return;
        }

        Model.battlePhase = BattlePhase.Ready;
        Model.turnCount++;

        var first = Model.battleEntities[0];    // 턴 반복문에서 가장 먼저 실행되는 ( 현재 턴을 잡고 있는 ) 엔티티를 가장 하위 턴을 잡도록 리스트를 섞는다

        Model.battleEntities.RemoveAt(0);
        Model.battleEntities.Add(first);

        Debug.Log($"Turn {Model.turnCount} 시작");
    }
    private void StopBattle()
    {
        GameManager.Instance.ExcuteBattleEvent(false);
    }
}
