using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public Player player;
    public IBattleEntity monster;

    public event Action battleEvent;

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

    public void StartBattle()
    {
        Debug.Log("예이 전투시작!");
        battleEvent?.Invoke();
        UIManager.Instance.battleWindow.WhenStartBattle();
        BattleManager.Instance.Battle.Encounter();
    }

    public void StopPlayer()
    {
        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        playerInput.DeactivateInput();
    }
    public void ActionPlayer()
    {
        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        playerInput.ActivateInput();
    }

}
