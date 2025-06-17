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

    // List<IBattleEntity> GetTargets();   // �ൿ�� ���� Ÿ���� ��ȯ�ϴ� �޼���
}

public class BattleModel : MonoBehaviour
{
    public bool BattleResult;
    public List<IBattleEntity> battleEntities = new List<IBattleEntity>(); // ������ �����ϴ� ���� �÷���

    public IBattleEntity nowTurnEntity;
    public IBattleEntity player;
    public IBattleEntity enemy;
    private PlayerInfo playerInfo;
    private MonsterInfo enemyInfo;
    public PlayerInfo PlayerInfo { get => playerInfo; set => playerInfo = value; }
    public MonsterInfo EnemyInfo { get => enemyInfo; set => enemyInfo = value; }

    public BattlePhase battlePhase;
    public int turnCount; // ���� �� ��


    private void Awake()
    {
        player = SkillManager.instance.TestPlayer.GetComponent<IBattleEntity>();
        enemy = SkillManager.instance.TestMonster.GetComponent<IBattleEntity>();
    }

}
