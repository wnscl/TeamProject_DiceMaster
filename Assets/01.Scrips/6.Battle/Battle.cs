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

    [SerializeField] private BattleModel model;

    Coroutine myCor;

    public BattleModel Model => model;


    [Button]
    /// <summary>
    /// ������ ������ �÷��̾ �������� �޼ҵ�
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
        IBattleEntity enemies = SkillManager.instance.TestMonster.GetComponent<IBattleEntity>();

        model.battleEntities.Add(enemies);
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

        GameManager.Instance.ActionPlayer();

        // ���� ���� �ʵ� �� ��ȯ
        BattleManager.Instance.IsBattleActive = true;
        model.isTurn = true;
        model.battlePhase = BattlePhase.Ready;

        AudioManager.Instance.ChangeAudio(AudioManager.Instance.audioPool.battleAudio, 2);

        myCor = StartCoroutine(Combat());
    }

    /// <summary>
    /// ��Ʋ ��, ������ ������ �����ϴ� �κ��� �޼ҵ�
    /// </summary>
    public IEnumerator Combat()
    {
        // ���� ���� ���� �� ������ ��Ʋ ���¸� Ȱ��ȭ
        while (BattleManager.Instance.IsBattleActive == true || Model.isTurn)
        {
            switch (Model.battlePhase)
            {
                case BattlePhase.Ready:
                    // �� ������ �ൿ�� ����
                    foreach (IBattleEntity entity in Model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // ���� ���� ���� ���� ����
                        yield return entity.ActionOnTurn(Model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Action;

                    Debug.Log("End Ready Phase");
                    break;
                case BattlePhase.Action:
                    // ������ �ൿ ����
                    foreach (IBattleEntity entity in Model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // ���� ���� ���� ���� ����

                        yield return entity.ActionOnTurn(Model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Result;

                    Debug.Log("End Action Phase");
                    break;
                case BattlePhase.Result:
                    // �� ���� ���� ó��

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

                    // �� ������ �ൿ ����� ó���ϰ�, ��Ʋ ���� ���θ� Ȯ��
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

        var first = Model.battleEntities[0];    // �� �ݺ������� ���� ���� ����Ǵ� ( ���� ���� ��� �ִ� ) ��ƼƼ�� ���� ���� ���� �⵵�� ����Ʈ�� ���´�

        Model.battleEntities.RemoveAt(0);
        Model.battleEntities.Add(first);

        Debug.Log($"Turn {Model.turnCount} ����");
    }
    private void StopBattle()
    {
        GameManager.Instance.ExcuteBattleEvent(false);
    }
}
