using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager _instance;
    public static BattleManager Instance { get { return _instance; } }

    private Battle battle;

    public Battle Battle
    {
        get { return battle; }
        set { battle = value; }
    }

    private IBattleable _player; // ��Ʋ �������̽��� ��ӹ޴� �÷��̾�
    public IBattleable Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private List<IBattleable> _enemies; // ��Ʋ �������̽��� ��ӹ޴� �� ���� ����Ʈ
    public List<IBattleable> Enemies
    {
        get { return _enemies; }
        set { _enemies = value; }
    }

    private bool isBattleActive = false;    // ��Ʋ ���� ���θ� ��Ÿ���� �÷���
    public bool IsBattleActive
    {
        get { return isBattleActive; }
        set { isBattleActive = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
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
