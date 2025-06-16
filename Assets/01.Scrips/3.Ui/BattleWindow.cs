using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleWindow : MonoBehaviour
{
    public Image playerHpBar;
    public Image monsterHpBar;
    public PlayerInfo playerInfo;
    public MonsterInfo monsterInfo;

    private void Awake()
    {
       
        
    }

    public void WhenStartBattle()
    {
        this.gameObject.SetActive(true);
        playerInfo = FindObjectOfType<PlayerInfo>().GetComponent<PlayerInfo>();
        monsterInfo = FindObjectOfType<MonsterInfo>().GetComponent<MonsterInfo>();
    }

    public void SetHPBar()
    {
        playerHpBar.fillAmount = (float)playerInfo.currentHp / playerInfo.maxHp;
        monsterHpBar.fillAmount = (float)monsterInfo.currentHp / monsterInfo.maxHp;
    }

    public void WhenEndBattle()
    {
        gameObject.SetActive(false);
        playerInfo = null;
        monsterInfo = null;
        playerHpBar.fillAmount = 1f;
        monsterHpBar.fillAmount = 1f;
    }
}