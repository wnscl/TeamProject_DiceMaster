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
    public  List<GameObject> slots = new List<GameObject>();
    public List<Sprite> diceSprites = new List<Sprite>();

    private bool isUp = false;

    public void OnClickUpDown( InputAction.CallbackContext context )
    { 
        if (context.phase != InputActionPhase.Started) return;
        
        if (!isUp)
        {
            AudioManager.Instance.PlayAudioOnce(UISFXEnum.Pause);
            transform.DOLocalMove(Vector3.down * 390f, 0.7f).SetEase(Ease.OutCubic);
            isUp = true;
        }
        else
        {
            AudioManager.Instance.PlayAudioOnce(UISFXEnum.Unpause);
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
                   if( slots[0].GetComponent<SkillShortcutSlot>().skillData !=null)
                   {Debug.Log(slots[0].GetComponent<SkillShortcutSlot>().skillData.Name);}
                   else{ SlotIsEmpty(1); }
                    break;
                case "2":
                    if( slots[1].GetComponent<SkillShortcutSlot>().skillData !=null)
                    {Debug.Log(slots[1].GetComponent<SkillShortcutSlot>().skillData.Name);}
                    else{ SlotIsEmpty(2); }
                    break;
                case "3":
                    if( slots[2].GetComponent<SkillShortcutSlot>().skillData !=null)
                    {Debug.Log(slots[2].GetComponent<SkillShortcutSlot>().skillData.Name);}
                    else { SlotIsEmpty(3); }
                    break;
                case "4":
                    if( slots[3].GetComponent<SkillShortcutSlot>().skillData !=null)
                    {Debug.Log(slots[3].GetComponent<SkillShortcutSlot>().skillData.Name);}
                    else { SlotIsEmpty(4); }
                    break;
                case "5":
                    if( slots[4].GetComponent<SkillShortcutSlot>().skillData !=null)
                    {Debug.Log(slots[4].GetComponent<SkillShortcutSlot>().skillData.Name);}
                    else { SlotIsEmpty(5); }
                    break;
                case "6":
                    if( slots[5].GetComponent<SkillShortcutSlot>().skillData !=null)
                    {Debug.Log(slots[5].GetComponent<SkillShortcutSlot>().skillData.Name);}
                    else { SlotIsEmpty(6); }

                    break;
            }
            
        }
    }

    private void SlotIsEmpty(int slotNum)
    {
        Debug.Log($"{slotNum}번슬롯 비어있음");
        AudioManager.Instance.PlayAudioOnce(UISFXEnum.Fail);
    }
}