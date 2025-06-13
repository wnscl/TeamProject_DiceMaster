using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public IBattleEntity player;
    public IBattleEntity monster;

    private void Awake()
    {
        Instance = this;
    }
}
