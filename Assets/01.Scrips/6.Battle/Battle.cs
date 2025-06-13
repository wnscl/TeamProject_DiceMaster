using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        if (BattleManager.Instance.Player.IsDead)
        {
            // to do : �й� ó��, Ȥ�� ���� ���� ó��
            BattleResult = false;

            EndBattle();
        }
        else if (BattleManager.Instance.Enemies.All(e => e.IsDead))
        {
            // to do : �¸� ó��, ������ ���, ����ġ ȹ�� ��
            BattleResult = true;

            EndBattle();
        }
        else
        {
            TurnShift();
        }
    }
}
