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
    public List<IBattleEntity> battleEntities; // 전투에 참가하는 유닛 컬렉션

    public GameObject battlePlayer;

    public IBattleEntity nowTurnEntity;
    public IBattleEntity player;
    public IBattleEntity enemy;

    public BattlePhase battlePhase;
    public int turnCount; // 현재 턴 수


    private void Awake()
    {
        player = battlePlayer.GetComponent<IBattleEntity>();

        EntityInfo info = player.GetEntityInfo();

        Debug.Log($"{info.name}");
    }

}
