using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class StatusPanel : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI evasionText;
    public TextMeshProUGUI pDefText;
    public TextMeshProUGUI mDefText;

    public void SetStatus()
    {
        var GM = GameManager.Instance.player.statHandler;
        nameText.text ="이름 :"+GM.statData. characterName;
        levelText.text ="레벨 : "+ GM.GetStat(StatType.Level);
        expText.text = $"경험치 : {GM.GetStat(StatType.Exp)}/{GM.GetStat(StatType.RequireExp)}";
        moneyText.text = "소지금 :" +GM.GetStat(StatType.Money);
        hpText.text = $"체력 : {GM.GetStat(StatType.Hp)}/{GM.GetStat(StatType.MaxHp)}";
       evasionText.text = $"회피력 :{GM.GetStat(StatType.Evasion)}";
        pDefText.text = $"물리방어력 :  {GM.GetStat(StatType.PhysicalDefense)}";
        mDefText.text =$"마법방어력 :  {GM.GetStat(StatType.MagicalDefense)} ";
    }

    private void OnEnable()
    {
        SetStatus();
    }
}
