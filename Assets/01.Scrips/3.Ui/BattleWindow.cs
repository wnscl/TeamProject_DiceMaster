using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class BattleWindow : MonoBehaviour
{
    PlayerInput playerInput;
    public Image playerHpBar;
    public Image monsterHpBar;
    public PlayerInfo playerInfo;
    public MonsterInfo monsterInfo;
    public TextMeshProUGUI statText;
    public GameObject endBattle;
    public TextMeshProUGUI anyKeyTxt;

    private void Awake()
    {
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        monsterInfo =FindObjectOfType<MonsterInfo>().GetComponent<MonsterInfo>();
        playerInfo =FindObjectOfType<BattlePlayerController>().GetComponent<PlayerInfo>();
        
    }

    [Button]
    public void WhenStartBattle()
    {
        this.gameObject.SetActive(true);
        playerInfo = FindObjectOfType<PlayerInfo>().GetComponent<PlayerInfo>();
        monsterInfo = FindObjectOfType<MonsterInfo>().GetComponent<MonsterInfo>();
        StartCoroutine(StartText());
    }

    public void SetHPBar()
    {
        playerHpBar.fillAmount = (float)playerInfo.currentHp / playerInfo.maxHp;
        monsterHpBar.fillAmount = (float)monsterInfo.currentHp / monsterInfo.maxHp;
    }

    [Button]
    public void WhenEndBattle()
    {
        StartCoroutine(EndText());
    }

    IEnumerator StartText()
    {
        statText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        statText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        statText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        statText.gameObject.SetActive(false);
    }

    IEnumerator EndText()
    {
        playerInput.DeactivateInput();
       
        endBattle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        anyKeyTxt.gameObject.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        playerInput.ActivateInput();
        anyKeyTxt.gameObject.SetActive(false);
        endBattle.SetActive(false);
        gameObject.SetActive(false);
        playerInfo = null;
        monsterInfo = null;
        playerHpBar.fillAmount = 1f;
        monsterHpBar.fillAmount = 1f;
    }
}