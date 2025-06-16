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


    ////private Monster enemy;
    //private bool BattleResult;
    //private List<IBattleEntity> battleEntities; // ������ �����ϴ� ���� �÷���
    //public IBattleEntity nowTurnEntity;


    //public IBattleEntity player;
    //public IBattleEntity monster; //�׽�Ʈ�� ���� ���Ϳ� �÷��̾� ���� �ʵ�


    //private BattlePhase battlePhase;
    //private int turnCount; // ���� �� ��

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
    /// ������ ������ �� ���͸� �������� �޼ҵ�
    /// </summary>
    public void GetEmenies()
    {
        // to do : ���� ��� �� ���� ����Ʈ�� �޾ƿ;� �� ���̴�
        IBattleEntity enemies = GameManager.Instance.monster;

        model.battleEntities.Add(enemies);
    }

    public void GetPlayer()
    {
        IBattleEntity player = model.player;

        model.battleEntities.Add(player);
    }

    /// <summary>
    /// ��Ʋ ���� ��, ���� �ܰ迡�� ��Ʋ�� �غ��ϴ� �޼ҵ�
    /// </summary>
    [Button]
    public void Encounter()
    {
        // ���� ���� �ʵ� �� ��ȯ
        BattleManager.Instance.IsBattleActive = true;
        model.battlePhase = BattlePhase.Ready;

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
            switch (model.battlePhase)
            {
                case BattlePhase.Ready:
                    // �� ������ �ൿ�� ����
                    foreach (IBattleEntity entity in model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // ���� ���� ���� ���� ����

                        yield return entity.ActionOnTurn(model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Action;
                    break;
                case BattlePhase.Action:
                    // ������ �ൿ ����
                    foreach (IBattleEntity entity in model.battleEntities)
                    {
                        model.nowTurnEntity = entity; // ���� ���� ���� ���� ����

                        yield return entity.ActionOnTurn(model.battlePhase);
                    }

                    model.battlePhase = BattlePhase.Result;
                    break;
                case BattlePhase.Result:
                    // �� ���� ���� ó��
                    foreach (IBattleEntity entity in model.battleEntities)
                    {
                        yield return entity.ActionOnTurn(model.battlePhase);
                    }

                    // �� ������ �ൿ ����� ó���ϰ�, ��Ʋ ���� ���θ� Ȯ��
                    CheckBattleEnd();
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

        if (model.battleEntities.Count == 0 || model.battleEntities.All(entity => entity is Player))
        {
            model.BattleResult = true; // �÷��̾� �¸�
            EndBattle();
        }
        else
        {
            // ��Ʋ�� ��� ���� ���� ���, ���� ������ �Ѿ��
            model.battlePhase = BattlePhase.Ready;
            model.turnCount++;
        }
    }
}
