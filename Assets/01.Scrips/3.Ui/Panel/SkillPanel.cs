using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    public List<ScriptableObject> skillSOData = new List<ScriptableObject>();
    public List<GameObject> skillPrefabs = new List<GameObject>();
    public List<GameObject> skillSlot;
    public GameObject slotPrefab;
public GameObject content;

    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, content.transform);
            skillSlot.Add(newSlot);
            
        }

    }
}