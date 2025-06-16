using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemInfo : MonoBehaviour
{
 public ItemSlot itemSlot;
 
 public Image itemIcon;
 public  TextMeshProUGUI itemName;
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
      this.gameObject.SetActive(false);
      ResetInfo();

      /*itemSlot = null;*/
    }
    else if (!this.gameObject.activeInHierarchy)
    {
      this.gameObject.SetActive(true);

      InitSetInfo();
    }
  }

 


  void ResetInfo()
  {
    
  }

  void InitSetInfo()
  {
    var iData = itemSlot.item.itemData;
    itemIcon.sprite = iData.itemIcon;
    itemName.text =iData.itemName;
    itemDescription.text = iData.description;

    if (iData is EquipmentItemData ID)
    {
      equipBtn.gameObject.SetActive(true);
      if (ID.isEquipped)
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
    else if (iData is ConsumableItemData CD)
    {
      equipBtn.gameObject.SetActive(false);
      useBtn.gameObject.SetActive(true);
      
    }

  }
}
