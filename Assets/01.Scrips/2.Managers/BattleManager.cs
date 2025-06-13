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

    private IBattleable _player; // 배틀 인터페이스를 상속받는 플레이어
    public IBattleable Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private List<IBattleable> _enemies; // 배틀 인터페이스를 상속받는 적 몬스터 리스트
    public List<IBattleable> Enemies
    {
        get { return _enemies; }
        set { _enemies = value; }
    }

    private bool isBattleActive = false;    // 배틀 진행 여부를 나타내는 플래그
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
