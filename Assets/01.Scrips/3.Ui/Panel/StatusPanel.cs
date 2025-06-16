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
       
        nameText.text ="이름 :"+GameManager.Instance.player.statHandler.statData. characterName;
        levelText.text ="레벨 : "+ GameManager.Instance.player.statHandler.GetStat(StatType.Level);
        expText.text = $"경험치 : {GameManager.Instance.player.statHandler.GetStat(StatType.Exp)}/{GameManager.Instance.player.statHandler.GetStat(StatType.RequireExp)}";
        moneyText.text = "소지금 :" +GameManager.Instance.player.statHandler.GetStat(StatType.Money);
        hpText.text = $"체력 : {GameManager.Instance.player.statHandler.GetStat(StatType.Hp)}/{GameManager.Instance.player.statHandler.GetStat(StatType.MaxHp)}";
       evasionText.text = $"회피력 :{GameManager.Instance.player.statHandler.GetStat(StatType.Evasion)}";
        pDefText.text = $"물리방어력 :  {GameManager.Instance.player.statHandler.GetStat(StatType.PhysicalDefense)}";
        mDefText.text =$"마법방어력 :  {GameManager.Instance.player.statHandler.GetStat(StatType.MagicalDefense)} ";
    }

    private void OnEnable()
    {
        SetStatus();
    }
}
