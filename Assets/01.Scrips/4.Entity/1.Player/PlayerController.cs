using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    public float stepDistance = 1f; //한번에 이동하는거리 데이터 만들기 전이라 임시로 여기 변수 두었습니다.


    public void OnMove(InputAction.CallbackContext context)
    {
        if(BattleManager.Instance.IsBattleActive)
        {  return;}
        
        if (context.phase == InputActionPhase.Started)
        {
            Vector2 input = context.ReadValue<Vector2>().normalized;
            transform.position += new Vector3(input.x, input.y, 0f) * stepDistance;
        }
    }
}