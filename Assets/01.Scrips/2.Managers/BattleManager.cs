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
