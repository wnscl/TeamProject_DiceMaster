using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        instance = this;    
    }







    public int[] RollDice()
    {
        int[] dice = new int[3];

        for (int i = 0; i < 3; i++)
        {
            dice[i] = UnityEngine.Random.Range(1, 7);    
        }

        return dice;
    }
}
