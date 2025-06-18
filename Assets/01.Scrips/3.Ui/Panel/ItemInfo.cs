using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public ItemSlot itemSlot;

    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemValue1;
    public TextMeshProUGUI itemValue2;
    public TextMeshProUGUI itemValue3;
    public TextMeshProUGUI itemValue4;
    public Image equipBtn;
    public TextMeshProUGUI equipBtnText;
    public Image useBtn;
    public Image throwBtn;
    public Image equipMark;

    public void InfoWIndowOnAndOff()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Debug.Log("끈다");
            this.gameObject.SetActive(false);
            ResetInfo();
            AudioManager.Instance.PlayAudioOnce(UISFXEnum.Unpause);
            itemSlot = null;
        }
        else if (!this.gameObject.activeInHierarchy)
        {
            InitSetInfo();
            this.gameObject.SetActive(true);
            AudioManager.Instance.PlayAudioOnce(UISFXEnum.UseItem);
        }
    }


    public void ResetInfo()
    {
        Debug.Log("슬롯 리셋");
        itemIcon.sprite = null;
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
    }

    public void InitSetInfo()
    {
        Debug.Log($"슬롯 정보 셋팅 :{itemSlot.item.ID}");
        var iData = itemSlot.item.itemData;
        itemIcon.sprite = iData.itemIcon;
        itemName.text = iData.itemName;
        itemDescription.text = iData.description;
        AudioManager.Instance.PlayAudioOnce(UISFXEnum.UseItem);

        if (iData is EquipmentItemData EI)
        {
            equipBtn.gameObject.SetActive(true);
            if (EI.isEquipped)
            {
                equipMark.gameObject.SetActive(true);
                equipBtnText.text = "장착 해제";
            }
            else
            {
                equipMark.gameObject.SetActive(false);
                equipBtnText.text = "장착";
            }

            useBtn.gameObject.SetActive(false);
        }
       else if (iData is DiceItemData DI)
        {
            equipBtn.gameObject.SetActive(true);
            if (DI.isEquipped)
            {
                equipMark.gameObject.SetActive(true);
                equipBtnText.text = "장착 해제";
            }
            else
            {
                equipMark.gameObject.SetActive(false);
                equipBtnText.text = "장착";
            }

            useBtn.gameObject.SetActive(false);
        }
        else if (iData is ConsumableItemData )
        {
            equipBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(true);
        }
        else if (iData is QuestItemData)
        {
            equipBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(false);
        }
    }
}