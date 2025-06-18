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
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI valueText;
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

            itemSlot = null;
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
        itemIcon.sprite = null;
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
        gradeText.text = string.Empty;
        typeText.text = string.Empty;
        valueText.text = string.Empty;
    }

    public void InitSetInfo()
    {
        Debug.Log($"슬롯 정보 셋팅 :{itemSlot.item.ID}");
        var iData = itemSlot.item.itemData;
        itemIcon.sprite = iData.itemIcon;
        itemName.text = iData.itemName;
        itemDescription.text = iData.description;

        // 장비 아이템인 경우
        if (itemSlot.item is EquipItemInstance equipInst)
        {
            equipBtn.gameObject.SetActive(true);

            if (equipInst.isEqEquipped)
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
            gradeText.text = $"등급 :  {GradeToK(itemSlot.item)}";
            typeText.text = "타입 : " + EquipTypeToK(itemSlot.item);
            if (itemSlot.item.itemData is EquipmentItemData ED)
            {
                valueText.text = EquipTypeValueToK(itemSlot.item) + ED.valueAmount;
            }
        }

        // 주사위 아이템인 경우
        else if (itemSlot.item is DiceItemInstance diceInst)
        {
            equipBtn.gameObject.SetActive(true);

            if (diceInst.isDcEquipped)
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
            gradeText.text = $"등급 :  {GradeToK(itemSlot.item)}";
            if (diceInst.itemData is DiceItemData DD)
            {
                valueText.text = "주사위 값 : " + DD.minValue + "~" + DD.maxValue;
                ;
            }
        }

        // 소비 아이템
        else if (itemSlot.item is ConsumableItemInstance CI)
        {
            equipBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(true);
            equipMark.gameObject.SetActive(false);
            gradeText.text = $"등급 :  {GradeToK(itemSlot.item)}";
            if (itemSlot.item is ConsumableItemData CD)
            {
                valueText.text = "회복량 : " + CD.valueAmount.ToString();
            }
        }

        // 퀘스트 아이템
        else if (itemSlot.item.itemData is QuestItemData)
        {
            equipBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(false);
            equipMark.gameObject.SetActive(false);
            gradeText.text = $"등급 :  {GradeToK(itemSlot.item)}";
        }
    }


    public void OnEquip()
    {
        IItem iitem = itemSlot.item;

        if (iitem is EquipItemInstance EI)
        {
            var equipSlot = UIManager.Instance.equipSlot;
            var rItem = equipSlot.ReturnIItem(iitem);
            if (rItem != null)
            {
                if (rItem.ID != iitem.ID)
                {
                    if (rItem is EquipItemInstance prevEquip)
                    {
                        prevEquip.isEqEquipped = false;
                        List<GameObject> otherSlot = UIManager.Instance.inventory.slots;
                        for (int i = 0; i < otherSlot.Count; i++)
                        {
                            ItemSlot slot = otherSlot[i].GetComponent<ItemSlot>();
                            if (slot != null && slot.item != null && slot.item.ID == rItem.ID)
                            {
                                otherSlot[i].GetComponent<ItemSlot>().SetSlot(rItem);
                            }
                        }

                        var slotImage = equipSlot.ReturnImage(rItem);
                        slotImage.sprite = null;

                        Color offColor = slotImage.color;
                        offColor.a = 0f;
                        slotImage.color = offColor;

                        equipSlot.ClearIItem(iitem);
                    }
                }
            }

            if (!EI.isEqEquipped)
            {
                EI.isEqEquipped = true;
                equipSlot.SetIItem(iitem);

                Image icon = equipSlot.ReturnImage(iitem);
                icon.sprite = iitem.itemData.itemIcon;
                Color color = icon.color;
                color.a = 1f;
                icon.color = color;
                if (EI.itemData is EquipmentItemData ED)
                {
                    GameManager.Instance.player.statHandler.ModifyStat(EquipTypeToValueType(iitem), ED.valueAmount);
                }
            }
            else
            {
                EI.isEqEquipped = false;
                equipSlot.ClearIItem(iitem);

                Image icon = equipSlot.ReturnImage(iitem);
                icon.sprite = null;
                Color color = icon.color;
                color.a = 0f;
                icon.color = color;
                if (EI.itemData is EquipmentItemData ED)
                {
                    GameManager.Instance.player.statHandler.ModifyStat(EquipTypeToValueType(iitem), -ED.valueAmount);
                }
            }
        }

        // 주사위 아이템 처리
        else if (iitem is DiceItemInstance DI)
        {
            var equipSlot = UIManager.Instance.equipSlot;
            var rItem = equipSlot.ReturnIItem(iitem);

            if (rItem != null)
            {
                if (rItem.ID != iitem.ID)
                {
                    if (rItem is DiceItemInstance prevDice)
                    {
                        prevDice.isDcEquipped = false;

                        // 인벤토리 내 동일 ID 슬롯 찾아서 UI 갱신
                        List<GameObject> otherSlot = UIManager.Instance.inventory.slots;
                        for (int i = 0; i < otherSlot.Count; i++)
                        {
                            ItemSlot slot = otherSlot[i].GetComponent<ItemSlot>();
                            if (slot != null && slot.item != null && slot.item.ID == rItem.ID)
                            {
                                slot.SetSlot(rItem);
                            }
                        }

                        var slotImage = equipSlot.ReturnImage(rItem);
                        slotImage.sprite = null;

                        Color offColor = slotImage.color;
                        offColor.a = 0f;
                        slotImage.color = offColor;

                        equipSlot.ClearIItem(iitem);
                    }
                }
            }

            if (!DI.isDcEquipped)
            {
                DI.isDcEquipped = true;
                equipSlot.SetIItem(iitem);

                Image icon = equipSlot.ReturnImage(iitem);
                icon.sprite = iitem.itemData.itemIcon;
                Color color = icon.color;
                color.a = 1f;
                icon.color = color;
            }
            else
            {
                DI.isDcEquipped = false;
                equipSlot.ClearIItem(iitem);

                Image icon = equipSlot.ReturnImage(iitem);
                icon.sprite = null;
                Color color = icon.color;
                color.a = 0f;
                icon.color = color;
            }
        }


        InitSetInfo(); // 버튼 텍스트 갱신 등
        UIManager.Instance.inventory.FindSameItem(iitem); // 인스턴스 정보 저장
        itemSlot.SetSlot(iitem);
    }

    public void OnUseHpItem()
    {
        if (itemSlot.item is ConsumableItemInstance CI)
        {
            if (CI.itemData is ConsumableItemData ID)
            {
                GameManager.Instance.player.statHandler.ModifyStat(StatType.Hp, ID.valueAmount);
                GameManager.Instance.player.statHandler.ObservingHp();
                UIManager.Instance.inventory.RemoveItem();
            }
        }
    }


    public string GradeToK(IItem iitem)
    {
        switch (iitem.itemData.grade)
        {
            case ItemGrade.Common:
                return "흔함";
            case ItemGrade.Uncommon:
                return UIManager.ColorText("보통", ColorName.cyan);
            case ItemGrade.Rare:
                return UIManager.ColorText("희귀", ColorName.purple);
            case ItemGrade.Unique:
                return UIManager.ColorText("유니크", ColorName.magenta);

            case ItemGrade.Legendary:
                return UIManager.ColorText("전설", ColorName.yellow);
            case ItemGrade.Quest:
                return UIManager.ColorText("퀘스트", ColorName.cyan);

            default:
                return "";
        }
    }

    public string EquipTypeToK(IItem iitem)
    {
        if (iitem is EquipItemInstance EI && EI.itemData is EquipmentItemData ED)
        {
            switch (ED.equipType)
            {
                case EquipType.Cloak:
                    return "망토";
                case EquipType.Clothet:
                    return "옷";
                case EquipType.Ring:
                    return "반지";
                case EquipType.Shoes:
                    return "신발";
                default:
                    return "알 수 없음";
            }
        }

        return "장비 아님";
    }

    public string EquipTypeValueToK(IItem iitem)
    {
        if (iitem is EquipItemInstance EI && EI.itemData is EquipmentItemData ED)
        {
            switch (ED.equipType)
            {
                case EquipType.Cloak:
                    return "물리방어력 : ";
                case EquipType.Clothet:
                    return "체력 : ";
                case EquipType.Ring:
                    return "마법방어력 : ";
                case EquipType.Shoes:
                    return "회피력 : ";
                default:
                    return "알 수 없음";
            }
        }

        return "장비 아님";
    }


    public StatType EquipTypeToValueType(IItem iitem)
    {
        if (iitem is EquipItemInstance EI && EI.itemData is EquipmentItemData ED)
        {
            switch (ED.equipType)
            {
                case EquipType.Cloak:
                    return StatType.PhysicalDefense;
                case EquipType.Clothet:
                    return StatType.MaxHp;
                case EquipType.Ring:
                    return StatType.MagicalDefense;
                case EquipType.Shoes:
                    return StatType.Evasion;
                default:
                    return StatType.None;
            }
        }

        return StatType.None;
    }
}