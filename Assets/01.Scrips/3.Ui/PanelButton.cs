using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
   UIManager um = UIManager.Instance;
   public void OnInventory()
   {
   um.inventory.gameObject.SetActive(true);
      um.statusPanel.gameObject.SetActive(true);
      
   }

   public void OnSkillWindow()
   {
      
   }

   public void OnStatusWindow()
   {
      
   }
}
