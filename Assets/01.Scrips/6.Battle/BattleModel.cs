using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattlePhase
{
    Ready,
    Action,
    Result
}

public interface IBattleEntity
{
    IEnumerator ActionOnTurn(BattlePhase phase);

    void GetDamage(int dmg);

    EntityInfo GetEntityInfo();

    // List<IBattleEntity> GetTargets();   // 행동에 대한 타겟을 반환하는 메서드
}

public class BattleModel : MonoBehaviour
{
    public bool BattleResult;
    public List<IBattleEntity> battleEntities = new List<IBattleEntity>(); // 전투에 참가하는 유닛 컬렉션

    public IBattleEntity nowTurnEntity;
    public IBattleEntity player;
    public IBattleEntity enemy;
    private PlayerInfo playerInfo;
    private MonsterInfo enemyInfo;
    public PlayerInfo PlayerInfo { get => playerInfo; set => playerInfo = value; }
    public MonsterInfo EnemyInfo { get => enemyInfo; set => enemyInfo = value; }

    public BattlePhase battlePhase;
    public int turnCount; // 현재 턴 수


    private void Awake()
    {
        player = SkillManager.instance.TestPlayer.GetComponent<IBattleEntity>();
        enemy = SkillManager.instance.TestMonster.GetComponent<IBattleEntity>();
    }

}
