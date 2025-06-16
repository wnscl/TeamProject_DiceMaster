using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public Player player;
    public IBattleEntity monster;

    private void Awake()
    {
        Instance = this;
    }
}
