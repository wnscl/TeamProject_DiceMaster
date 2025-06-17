using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;

public class SkillShortCut : MonoBehaviour
{
    public GameObject slotsContainer;
    public GameObject slotPrefab;
    public static List<GameObject> slots = new List<GameObject>();
    public List<Sprite> diceSprites = new List<Sprite>();

    private bool isUp = false;

    public void OnClickUpDown()
    {
        if (!isUp)
        {
            transform.DOLocalMove(Vector3.down * 390f, 0.7f).SetEase(Ease.OutCubic);
            isUp = true;
        }
        else
        {
            transform.DOLocalMove(Vector3.down * 690, 0.7f)
                .SetEase(Ease.OutCubic);
            isUp = false;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < diceSprites.Count; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsContainer.transform);
            slots.Add(newSlot);
        }

        
    }

    private void Start()
    {
        SetupSlots();
    }

    public void SetupSlots()
    {
        for (int i = 0; i < diceSprites.Count; i++)
        {
            Image slotIcon = slots[i].transform.Find("Icon").GetComponent<Image>();
            slotIcon.sprite = diceSprites[i];
        }
    }


    public void OnUsingSkill(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        { 
            string key = context.control.name;
            
            switch (key)
            {
                case "1":
                    Debug.Log("1번슬롯 스킬 사용");
                    break;
                case "2":
                    Debug.Log("2번슬롯 스킬 사용");
                    break;
                case "3":
                    Debug.Log("3번슬롯 스킬 사용");
                    break;
                case "4":
                    Debug.Log("4번슬롯 스킬 사용");
                    break;
                case "5":
                    Debug.Log("5번슬롯 스킬 사용");
                    break;
                case "6":
                    Debug.Log("6번슬롯 스킬 사용");
                    break;
            }
            
        }


    }
    
    
    
    
    
    
    
}