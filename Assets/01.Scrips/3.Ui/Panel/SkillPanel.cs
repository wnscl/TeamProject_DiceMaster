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

    public Dictionary<int, bool> saveSkill = new Dictionary<int, bool>();
    public GameObject slotPrefab;
    public GameObject content;
   
    private void Start()
    {
        for (int i = 0; i < skillManager.skillDatas.Length; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, content.transform);

            SkillSlot ss = newSlot.GetComponent<SkillSlot>();

            if (ss != null)
            {
                ss.skillData = skillManager.skillDatas[i];
            }

            ss.setSetSkillSlot();
            skillSlot.Add(newSlot);
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
    
    
}