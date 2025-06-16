using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkillShortCut : MonoBehaviour
{
    public GameObject slotsContainer;
    public GameObject slotPrefab;
    public List<GameObject> slots = new List<GameObject>();
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
}