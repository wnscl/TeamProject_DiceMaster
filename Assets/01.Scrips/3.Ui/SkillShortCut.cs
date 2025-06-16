using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkillShortCut : MonoBehaviour
{
    public GameObject ShortCut;
    private bool isUp = false;

    public void OnClickUpDown()
    {
        if (!isUp)
        {
            ShortCut.transform.DOLocalMove(Vector3.down * 390f, 0.7f).SetEase(Ease.OutCubic);
            isUp = true;
        }
        else
        {
            ShortCut.transform.DOLocalMove(Vector3.down * 690, 0.7f)
                .SetEase(Ease.OutCubic);
            isUp = false;
        }
    }
}