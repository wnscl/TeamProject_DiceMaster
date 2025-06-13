using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

/*
1. ������ [ ���� ], [ ���� ], [ ��� ] �� 3�ܰ�� ����������.
1-1. ���� ���� �̺�Ʈ�� ����, BattleEvent�� ����, ����, ��� ��� ���̴� ��Ÿ�� �� �ִ�

2. [ ���� ] �ܰ��� ������ ����Ʈ
�� ��Ʋ �ý����� �⺻������ <���۷κ�����> �� �ý����� ���۷����� �����Ѵ�
2-1. �غ� : �̹� �Ͽ� ������ �� ������ �غ��ϴ� �ܰ�.
2-2. ��� : ������ ���ۿ� ���� ������ �ִٸ� �̿� ���� ó���� �Ѵ�.
2-2. ���� : [ �غ� ] �ܰ迡�� ������ �ൿ�� �Ѵ�. ���� ���� ���� �ൿ�� �켱�Ѵ�.
2-3. ��ȯ : [ ���� ] �ܰ��� ����� Ȯ���ϰ� ���� ��ȯ�Ѵ�.

3. �� ������ �� ����
3-1. �غ� : �ڽ��� �ൿ�� �����Ѵ�. �ൿ�� ũ�� ����, ���, ȸ��, ������ ���, ��ȹ�� �����Ѵٸ� ���� ���� �ൿ�� ���Եȴ�.
3-2. ��� : �ֻ��� �����, ũ��Ƽ��, ȸ�� Ȯ�� ���� ���� ������ �ǽ��Ѵ�.
3-3. ���� : ������ ������ ���� ������ �ǽ��Ѵ�. �⺻������ ���� ���� ���� �ൿ�� �켱������, ���� ��ų�̳� Ư�� �������ͽ��� �ִٸ� �������� ���ѱ� ���� �ִ�.
3-4. ��ȯ : �� ������ ���� ���� �� ���� �ܰ迡���� ����, ������� ���� ó���� �����ϰ� ( ��, ���� �� ) ���� ��ȯ�Ѵ�.

4. �� ��Ʋ �ܰ躰 �� ����
4-1. ���� : �� ���ֺ� �������ͽ��� �о�� ��Ʋ�� �غ��Ѵ�. ��Ʋ ���� ���� ����� �������̳� ������ �ִٸ� �� �ܰ迡�� ����ȴ� ( ��, �������ͽ� ������ ���� ������ �̹� �÷��̾� ���� ��ũ��Ʈ���� ������ ��ġ�� ������ ���� ���̴� �⺻�����δ� ������ ����ȴٰ� �����ϸ� �� ���̴�. ���� ��� "��Ʋ ���� ��, ����� ����" �� ���� ȿ���� �� �ܰ迡�� ����ȴ� )
4-2. ���� : 2, 3 �� ���� ����
4-3. ��� : �¸�, �й迡 ���� ����� ó���Ѵ�. ���丮 �̺�Ʈ�� �����ϰų� �б��� ��ȯ, ������ ȹ�� ���� ó���� �����Ѵ�.
 */

public enum BattlePhase
{
    Ready,
    Action,
    Result
}

public interface IBattleEntity
{
    IEnumerator ActionOnTurn(BattlePhase phase);
}

public class Battle : MonoBehaviour
{
    //private Monster enemy;
    private bool BattleResult;

    private List<IBattleEntity> battleEntities; // ������ �����ϴ� ���� �÷���

    private GameObject player;
    private List<GameObject> enemies;

    BattlePhase battlePhase;

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
    /// ������ ������ �� ���͸� �������� �޼ҵ�
    /// </summary>
    public void GetEmenies()
    {
        // List<IBattleEntity> enemis = MonsterManager.Instance.enemies.GetEncounterEnemies();
    }

    public void GetPlayer()
    {
        // player = PlayerManager.Instance.GetPlayer(); // �÷��̾ �������� �޼ҵ�
        // player.GetComponent<IBattleEntity>().ActionOnTurn(BattlePhase.Ready); // �÷��̾��� �ൿ�� �غ� �ܰ�� ����
    }

    /// <summary>
    /// ��Ʋ ���� ��, ���� �ܰ迡�� ��Ʋ�� �غ��ϴ� �޼ҵ�
    /// </summary>
    public void Encounter()
    {
        BattleManager.Instance.IsBattleActive = true;
    }

    /// <summary>
    /// ��Ʋ ��, ������ ������ �����ϴ� �κ��� �޼ҵ�
    /// </summary>
    public void Combat()
    {

    }

    /// <summary>
    /// ��Ʋ ���� ��, ����� ó���ϴ� �޼ҵ�
    /// </summary>
    public void EndBattle()
    {
        BattleManager.Instance.IsBattleActive = false;
    }

    /// <summary>
    /// ������ �ൿ�� �����ϴ� �޼ҵ�
    /// </summary>
    /// <param name="unit"></param>
    public void SetAction()
    {

    }

    /// <summary>
    /// �� ������ ������ �ൿ�� ���� ���� ó���� �ϴ� �޼ҵ�
    /// </summary>
    public void ProcessAction()
    {

    }

    /// <summary>
    /// ������ �ൿ�� �����ϴ� �޼ҵ�
    /// </summary>
    public void ExcuteAction()
    {

    }

    /// <summary>
    /// ���� ���� ��ȯ�ϴ� �޼ҵ�
    /// ������ �� �÷��׸� ������ �ִ� ������ �� �÷��׸� �����ϰ�, �� �÷��װ� ���� ������ �� �÷��׸� �����Ѵ�
    /// </summary>
    public void TurnShift()
    {
        
    }

    /// <summary>
    /// ��Ʋ ����� Ȯ���ϴ� �޼ҵ�
    /// </summary>
    /// <returns></returns>
    private void CheckBattleEnd()
    {
        // to do : �÷��̾�� ���� ü���� üũ�ϰ�, ��� ���� ü���� 0 ���ϰ� �Ǿ����� üũ
        // �÷��̾ ü���� 0�� �Ǿ��� ���, ���� ���� ó�� Ȥ�� ��Ʋ �й� ó��
        // ��� ���� ü���� 0�� �Ǿ��� ���, ��Ʋ �¸� ó��
        // �÷��̾�� �� �� ü���� ���� ������ ���� ���, �� ��ȯ
    }

    /*
     * ���� ��, �� Entity�� �ش� �޼ҵ带 �ݺ� �����Ѵ�
     * ��Ʋ�� �������� ��, battlePhase�� ���� �ൿ�� �����Ѵ�
     * 
     * Ready : �� ��ƼƼ�� �ൿ ����
     * 
     * Action : Ÿ���� ���� �ൿ ����
     * 
     * Result : ���� Ȥ�� ����� ����, ��Ʋ ���� ���� Ȯ��, �� ��ȯ
     * 
     */
    private IEnumerator TurnReapter()
    {
        while (BattleManager.Instance.IsBattleActive)
        {
            switch (battlePhase)
            {
                case BattlePhase.Ready:
                    yield return new WaitUntil(() => BattleManager.Instance.IsBattleActive == true);
                    // yield return new WaitUntil(() => isStageRunning);

                    battlePhase = BattlePhase.Action;   // ����� ��ȯ�Ѵ�
                    break;

                case BattlePhase.Action:
                    // yield return new WaitUntil(() => StageEnd());
                    // yield return new WaitUntil(() => isStageRunning);
                    break;

                case BattlePhase.Result:
                    // yield return new WaitUntil(() => StageEnd());
                    // yield return new WaitUntil(() => isStageRunning);
                    break;
            }
        }
        yield break;
    }
}
