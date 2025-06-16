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
   }

   public void OnSkillWindow()
   {
      um.inventory.gameObject.SetActive(false);
      um.statusPanel.gameObject.SetActive(false);
      um.skillPanel.gameObject.SetActive(true);  
   }

   public void OnStatusWindow()
   {
      um.inventory.gameObject.SetActive(false);
      um.statusPanel.gameObject.SetActive(true);
      um.skillPanel.gameObject.SetActive(false);  
   }
}
