using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public IBattleEntity monster;
    public bool isPlayerWin;

    public int monsterIndex;

    public event Action<bool> battleEvent;

    private void Awake()
    {
        Instance = this;
        isPlayerWin = false;
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
