using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
   private UIManager um;
   void Start()
   {
      um = UIManager.Instance;
   }

   
   public void OnInventory()
   {
   um.inventory.gameObject.SetActive(true);
      um.statusPanel.gameObject.SetActive(false);
   um.skillPanel.gameObject.SetActive(false); 
   um.skillInfo.gameObject.SetActive(false);
   um.itemInfo.gameObject.SetActive(false);
   }

   public void OnSkillWindow()
   {
      um.inventory.gameObject.SetActive(false);
      um.statusPanel.gameObject.SetActive(false);
      um.skillPanel.gameObject.SetActive(true); 
      um.skillInfo.gameObject.SetActive(false);
      um.itemInfo.gameObject.SetActive(false);
   }

   public void OnStatusWindow()
   {
      um.inventory.gameObject.SetActive(false);
      um.statusPanel.gameObject.SetActive(true);
      um.skillPanel.gameObject.SetActive(false);  
      um.skillInfo.gameObject.SetActive(false);
      um.itemInfo.gameObject.SetActive(false);
   }
}
