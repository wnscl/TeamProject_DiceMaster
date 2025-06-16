using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
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
3-2. ��� ( �غ� �ܰ迡�� ���� ) : �ֻ��� �����, ũ��Ƽ��, ȸ�� Ȯ�� ���� ���� ������ �ǽ��Ѵ�.
3-3. ���� : ������ ������ ���� ������ �ǽ��Ѵ�. �⺻������ ���� ���� ���� �ൿ�� �켱������, ���� ��ų�̳� Ư�� �������ͽ��� �ִٸ� �������� ���ѱ� ���� �ִ�.
3-4. ��ȯ : �� ������ ���� ���� �� ���� �ܰ迡���� ����, ������� ���� ó���� �����ϰ� ( ��, ���� �� ) ���� ��ȯ�Ѵ�.

4. �� ��Ʋ �ܰ躰 �� ����
4-1. ���� : �� ���ֺ� �������ͽ��� �о�� ��Ʋ�� �غ��Ѵ�. ��Ʋ ���� ���� ����� �������̳� ������ �ִٸ� �� �ܰ迡�� ����ȴ� ( ��, �������ͽ� ������ ���� ������ �̹� �÷��̾� ���� ��ũ��Ʈ���� ������ ��ġ�� ������ ���� ���̴� �⺻�����δ� ������ ����ȴٰ� �����ϸ� �� ���̴�. ���� ��� "��Ʋ ���� ��, ����� ����" �� ���� ȿ���� �� �ܰ迡�� ����ȴ� )
4-2. ���� : 2, 3 �� ���� ����
4-3. ��� : �¸�, �й迡 ���� ����� ó���Ѵ�. ���丮 �̺�Ʈ�� �����ϰų� �б��� ��ȯ, ������ ȹ�� ���� ó���� �����Ѵ�.
 */


public class Battle : MonoBehaviour
{

    private BattleModel model;

    public BattleModel Model
    {
        get
        {
            if (model == null)
            {
                model = GetComponent<BattleModel>();
            }

            return model;
        }
        set => model = value;
    }

    void Start()
    {
        model = GetComponent<BattleModel>();
    }

    void Update()
    {
        
    }

    [Button]
    /// <summary>
    /// ������ ������ �÷��̾ �������� �޼ҵ�
    /// </summary>
    public void GetPlayer()
    {
        IBattleEntity player = GameManager.Instance.player as IBattleEntity;

        if (player != null)
        {
            Model.battleEntities.Add(player);
            // model.PlayerInfo = player.GetEntityInfo() as PlayerInfo;
        }
        else
        {
            Debug.LogWarning("�÷��̾� �ν��Ͻ� ��濡 �����Ͽ����ϴ�.");
        }
    }

    [Button]
    /// <summary>
    /// ������ ������ �� ���͸� �������� �޼ҵ�
    /// </summary>
    public void GetEmenies()
    {
        // to do : ���� ��� �� ���� ����Ʈ�� �޾ƿ;� �� ���̴�
        IBattleEntity enemies = GameManager.Instance.monster;

        Model.battleEntities.Add(enemies);
        // model.EnemyInfo = enemies.GetEntityInfo() as MonsterInfo;
    }

    /// <summary>
    /// ��Ʋ ���� ��, ���� �ܰ迡�� ��Ʋ�� �غ��ϴ� �޼ҵ�
    /// </summary>
    [Button]
    public void Encounter()
    {
        // ��Ƽ ��Ʈ����Ʈ ��ư�� ���� �׽�Ʈ�� battleEntities �ʱ�ȭ
        GetPlayer();
        GetEmenies();

        // ���� ���� �ʵ� �� ��ȯ
        BattleManager.Instance.IsBattleActive = true;
        Model.battlePhase = BattlePhase.Ready;

        StartCoroutine(Combat());
    }

    /// <summary>
    /// ��Ʋ ��, ������ ������ �����ϴ� �κ��� �޼ҵ�
    /// </summary>
    public IEnumerator Combat()
    {
        // ���� ���� ���� �� ������ ��Ʋ ���¸� Ȱ��ȭ
        while (BattleManager.Instance.IsBattleActive == true)
        {
            switch (Model.battlePhase)
            {
                case BattlePhase.Ready:
                    // �� ������ �ൿ�� ����
                    foreach (IBattleEntity entity in Model.battleEntities)
                    {
                        Model.nowTurnEntity = entity; // ���� ���� ���� ���� ����

                        yield return entity.ActionOnTurn(Model.battlePhase);
                    }

                    Model.battlePhase = BattlePhase.Action;

                    Debug.Log("End Ready Phase");
                    break;
                case BattlePhase.Action:
                    // ������ �ൿ ����
                    foreach (IBattleEntity entity in Model.battleEntities)
                    {
                        Model.nowTurnEntity = entity; // ���� ���� ���� ���� ����

                        yield return entity.ActionOnTurn(Model.battlePhase);
                    }

                    Model.battlePhase = BattlePhase.Result;

                    Debug.Log("End Action Phase");
                    break;
                case BattlePhase.Result:
                    // �� ���� ���� ó��
                    foreach (IBattleEntity entity in Model.battleEntities)
                    {
                        yield return entity.ActionOnTurn(Model.battlePhase);
                    }

                    // �� ������ �ൿ ����� ó���ϰ�, ��Ʋ ���� ���θ� Ȯ��
                    CheckBattleEnd();

                    Debug.Log("End Result Phase");
                    break;
            }
        }
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
        // to do : ����Ʈ�� �÷��̾ ���ٸ� ���� ������Ű�� ���� ����� ����ص���
        // ��Ʋ�� ������ ���� ����Ʈ�� �÷��̾ �������� ���, ��Ʋ�� �÷��̾��� �¸��� ó���ϰ� ��Ʋ�� �����Ѵ�

        if (Model.battleEntities.Count == 0 || Model.battleEntities.All(entity => entity is Player))
        {
            Model.BattleResult = true; // �÷��̾� �¸�
            EndBattle();
        }
        else
        {
            // ��Ʋ�� ��� ���� ���� ���, ���� ������ �Ѿ��
            RotateTurn();
        }
    }

    private void RotateTurn()
    {
        Model.battlePhase = BattlePhase.Ready;
        Model.turnCount++;

        var first = Model.battleEntities[0];    // �� �ݺ������� ���� ���� ����Ǵ� ( ���� ���� ��� �ִ� ) ��ƼƼ�� ���� ���� ���� �⵵�� ����Ʈ�� ���´�

        Model.battleEntities.RemoveAt(0);
        Model.battleEntities.Add(first);

        Debug.Log($"Turn {Model.turnCount} ����");
    }
}
