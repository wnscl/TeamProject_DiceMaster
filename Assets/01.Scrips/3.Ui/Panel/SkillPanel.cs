using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] SkillManager skillManager;

    public List<SkillDataSo> skillSOData = new List<SkillDataSo>();
    public List<GameObject> skillPrefabs = new List<GameObject>();
    public List<GameObject> skillSlot = new List<GameObject>();
    public List<GameObject> skillSlotPivot = new List<GameObject>();
    
    public GameObject pivotPrefab;
    public GameObject slotPrefab;
    public GameObject content;

    private void Start()
    {
        for (int i = 0; i < skillManager.skillDatas.Length; i++)
        {
            GameObject newPivot = Instantiate(pivotPrefab, content.transform);
            skillSlotPivot.Add(newPivot);
           GameObject newSlot = Instantiate(slotPrefab, skillSlotPivot[i].transform); 

            SkillSlot ss = newSlot.GetComponent<SkillSlot>();

            if (ss != null)
            {
                ss.skillData = skillManager.skillDatas[i];
            }

            ss.setSetSkillSlot();
            skillSlot.Add(newSlot);
        }

        for (int i = 0; i < 9; i++)
        {
            GetSkill(skillManager.skillDatas[i]);
        }

       
    }

    public void GetSkill(SkillDataSo newSkill)
    {
        foreach (GameObject slot in skillSlot)
        {
            SkillSlot ss = slot.GetComponent<SkillSlot>();
            if (ss != null)
            {
                ss.UnlockSkill(newSkill);
            }
        }
    }

    [Button]
    public void testGetSkill()
    {
        GetSkill(skillManager.skillDatas[0]);
    }

    [Button]
    public void testGetSkill2()
    {
        GetSkill(skillManager.skillDatas[1]);
    }
    
    
    public List< bool> saveSkill = new List< bool>();

    public void SaveSkill()
    {
      saveSkill.Clear();
       
      for (int i = 0; i < skillSlot.Count; i++)
        {
            saveSkill[i] = skillSlot[i].GetComponent<SkillSlot>().isLocked;
            
            
        }

    }


}