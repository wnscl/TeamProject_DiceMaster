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
    public bool isPlayerWin;

    public int monsterIndex;

    public event Action<bool> battleEvent;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            isPlayerWin = false;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ExcuteBattleEvent(bool startOrEnd)
    {
        battleEvent?.Invoke(startOrEnd);
        switch (startOrEnd)
        {
            case true:
                UIManager.Instance.battleWindow.WhenStartBattle();
                BattleManager.Instance.Battle.Encounter();
                break;

            case false:
                UIManager.Instance.battleWindow.WhenEndBattle();
                AudioManager.Instance.ChangeAudio(AudioManager.Instance.audioPool.BackGroundAudio[StageManager.Instance.currentStage]);
                break;
        }
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
