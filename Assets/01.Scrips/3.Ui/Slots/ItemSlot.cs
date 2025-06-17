using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public IItem item;


    public Image icon;
    public Image selectMask;
    public GameObject equipmentMark;
    public GameObject stackAmountMark;
    public TextMeshProUGUI stackAmountText;

    void Awake()
    {
    }

    public void OnClickInfo()
    {
        if (item == null)
        {
            Debug.Log("빈 슬롯");
            return;
        }

        if (UIManager.Instance.itemInfo.gameObject.activeInHierarchy &&
            UIManager.Instance.itemInfo.itemSlot.item.ID != item.ID)
        {
            Debug.Log("다른 슬롯 클릭 정보 갱신");
            UIManager.Instance.itemInfo.itemSlot = this;
            UIManager.Instance.itemInfo.InitSetInfo();
            
            return;
        }
        
        UIManager.Instance.itemInfo.itemSlot = this;
        Debug.Log("여기로 나오니?");
        UIManager.Instance.itemInfo.InfoWIndowOnAndOff();
        
        
          
       
    }
    //이놈이 문제임
    /*private static void ExecuteTasks()
    {
        if (!(SynchronizationContext.Current is UnitySynchronizationContext current))
            return;
        current.Exec();
    }*/

    public void SetSlot(IItem item)
    {
        icon.sprite = item.itemData.itemIcon;

        if (item.itemData is EquipmentItemData EI)
        {
            if (EI.isEquipped)
            {
                equipmentMark.SetActive(true);
            }
            else
            {
                equipmentMark.SetActive(false);
            }
        }

        if (item.itemData is ConsumableItemData CI)
        {
            if (CI.isStackable)
            {
                stackAmountMark.SetActive(true);
                stackAmountText.text = CI.stackAmount.ToString();
            }
            else
            {
                stackAmountMark.SetActive(false);
            }
        }

        if (item.itemData is DiceItemData DI)
        {
            if (DI.isEquipped)
            {
                equipmentMark.SetActive(true);
            }
            else
            {
                equipmentMark.SetActive(false);
            }
        }
    }

    public void ONDestroySlot()
    {
        if (item.itemData is EquipmentItemData EI)
        {
            if (EI.isEquipped)
            {
                Debug.Log("아이템 장착을 해제하쇼");
                UIManager.Instance.SystemMessage("먼저 아이템 장착을 해제하세요");
                return;
            }
        }
        if (item.itemData is DiceItemData DI)
        {
            if (DI.isEquipped)
            {
                Debug.Log("아이템 장착을 해제하쇼");
                UIManager.Instance.SystemMessage("먼저 아이템 장착을 해제하세요");
                return;
            }
        }

        UIManager.Instance.itemInfo.InfoWIndowOnAndOff();
        ResetSlot();
        UIManager.Instance.inventory.slots.Remove(this.gameObject);
        Destroy(this.gameObject);
        UIManager.Instance.inventory.slot = null;
    }

    public void ResetSlot()
    {
        item = null;
        icon.sprite = null;
        equipmentMark.SetActive(false);
        stackAmountMark.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        selectMask.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectMask.gameObject.SetActive(false);
    }
}