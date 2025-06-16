using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
 public  StatHandler statHandler;
public PlayerInfo playerInfo;

  private void Awake()
  {
   statHandler = GetComponent<StatHandler>();
   playerInfo = GetComponent<PlayerInfo>();
  }


 
 
}
