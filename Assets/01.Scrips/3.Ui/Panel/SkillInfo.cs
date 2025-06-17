using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SkillInfo : MonoBehaviour
{
    public SkillSlot skillSlot;
    public Image icon;
    public TextMeshProUGUI skillDescription;
    public TextMeshProUGUI skillName;

    public void InfoWIndowOnAndOff()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            ResetInfo();

            skillSlot = null;
        }
        else if (!this.gameObject.activeInHierarchy)
        {
            InitSetInfo();
            this.gameObject.SetActive(true);
        }
    }

    public void ResetInfo()
    {
        Debug.Log("슬롯 리셋");
        icon.sprite = null;
        skillName.text = string.Empty;
        skillDescription.text = string.Empty;
    }

    public void InitSetInfo()
    {
        Debug.Log($"슬롯 정보 셋팅 :{skillSlot.skillData.name}");
        icon.sprite = skillSlot.skillData.Icon;
        skillName.text = skillSlot.skillData.name;
        skillDescription.text = skillSlot.skillData.Discription;
    }
}