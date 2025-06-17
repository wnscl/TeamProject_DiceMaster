using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleResultUI : MonoBehaviour
{
    public TextMeshProUGUI getItem;
    public TextMeshProUGUI getSkill;
    public TextMeshProUGUI getGold;
    public TextMeshProUGUI getExp;


    public void SetResultText()
    {
        getItem.text = "";
        getSkill.text = "";
        getGold.text = "";
        getExp.text = "";


    }

}