using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��带 ��ƿ��� Ŭ����Ǵ� ����Ʈ
/// </summary>
public class GoldQuest : Quest
{
    [SerializeField] int requireGold; // ����Ʈ �Ϸῡ �ʿ��� ���

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    /// <summary>
    /// ����Ʈ �Ϸ� ������ �˻��ϴ� �޼ҵ�
    /// �䱸ġ��ŭ ��带 ��Ҵ��� Ȯ���ϰ�, ��Ҵٸ� ����Ʈ�� �Ϸ�
    /// </summary>
    public override void CheckCondition()
    {
        base.CheckCondition();

        int playerGold = GameManager.Instance.player.statHandler.GetStat(StatType.Money);   // ���� �÷��̾� ���

        Debug.Log($"���� �÷��̾� ���: {playerGold}, �䱸 ���: {requireGold}");

        if (playerGold >= requireGold)
        {
            GameManager.Instance.player.statHandler.ModifyStat(StatType.Money, -requireGold);   // �÷��̾� ��带 �䱸ġ��ŭ ����

            CompleteQuest();
        }
        else
        {
            Debug.Log("����Ʈ ���� ������: ��� ����");
            // to do : UI�� ����Ʈ ���� �޼��� ����� ��
        }
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();

        foreach (QuestData item in QuestManager.Instance.PlayerQuests)
        {
            if (item.QuestId == questData.QuestId && questData.IsAccepted)
            {
                questData.IsCompleted = true; // ����Ʈ �Ϸ� ���·� ����
                item.IsCompleted = true; // ����Ʈ �Ϸ� ���·� ���� / to do : ���� ����� IsCompleted ������ �ʿ� ����

                QuestManager.Instance.PlayerQuests.Remove(item);    // �÷��̾� ����Ʈ ����Ʈ���� ����Ʈ ����
                QuestManager.Instance.CompletedQuests.Add(item);    // �Ϸ��� ����Ʈ ��Ͽ� �߰�

                Debug.Log($"{item.QuestName} ����Ʈ �Ϸ�");
                break;
            }
        }
    }
}
