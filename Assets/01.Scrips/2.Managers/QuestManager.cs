using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ü ����Ʈ ���� �Ŵ���
/// </summary>
public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("QuestManager").AddComponent<QuestManager>();
            }

            return _instance;
        }
    }

    private List<QuestData> playerQuests; // ���� ���� ���� ����Ʈ ���

    public List<QuestData> PlayerQuests
    {
        get { return playerQuests; }
        set { playerQuests = value; }
    }

    private List<QuestData> completedQuests; // �Ϸ��� ����Ʈ ��� ( ����Ʈ ���� �� )
    public List<QuestData> CompletedQuests
    {
        get { return completedQuests; }
        set { completedQuests = value; }
    }

    // to do : ��ü ����Ʈ�� �����ϴ� �÷����� �߰��Ͽ� ���� ���� ����Ʈ, �Ϸ��� ����Ʈ ���� ������ ������ Ȯ���� �� �ֵ��� �����غ� ��

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            playerQuests = new List<QuestData>();
            completedQuests = new List<QuestData>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
