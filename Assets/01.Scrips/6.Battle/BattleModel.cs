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
}

public class BattleModel : MonoBehaviour
{
    public bool BattleResult;
    public List<IBattleEntity> battleEntities; // ������ �����ϴ� ���� �÷���

    public GameObject battlePlayer;

    public IBattleEntity nowTurnEntity;
    public IBattleEntity player;
    public IBattleEntity enemy;

    public BattlePhase battlePhase;
    public int turnCount; // ���� �� ��


    private void Awake()
    {
        player = battlePlayer.GetComponent<IBattleEntity>();

        EntityInfo info = player.GetEntityInfo();

        Debug.Log($"{info.name}");
    }

}
