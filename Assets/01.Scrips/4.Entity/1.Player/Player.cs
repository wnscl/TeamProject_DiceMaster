using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Player : MonoBehaviour,IBattleEntity
{
 public  StatHandler statHandler;
public PlayerInfo playerInfo;

  private void Awake()
  {
   statHandler = GetComponent<StatHandler>();
   playerInfo = GetComponent<PlayerInfo>();
  }
  public IEnumerator ActionOnTurn(BattlePhase phase)
  {
      Debug.Log("Player's turn action executed.");
      return null;
  }

  
  
  public void GetDamage(int dmg)
  { 
      int Rd = UnityEngine. Random.Range(0, 100);

      if (statHandler.GetStat(StatType.Evasion) > Rd)
      {
          UIManager.Instance.SystemMessage("공격을 회피했습니다."); 
          return;
      }
        
      playerInfo.currentHp -= dmg;
      Die();
  }

  public bool isDead { get; private set; }

  void Die()
  {
      if (playerInfo.currentHp<=0)
      {
          isDead = true;
      };

  }

  public EntityInfo GetEntityInfo()
  {
      throw new NotImplementedException();
  }

 
 
}
