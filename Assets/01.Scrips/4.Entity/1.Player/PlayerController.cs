using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
  [SerializeField]  private float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (BattleManager.Instance.IsBattleActive)
        {
            rb.velocity = Vector3.zero;
            return;
        }

    
        rb.velocity = new Vector3(moveInput.x, moveInput.y, 0f) * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (BattleManager.Instance.IsBattleActive)
        {
            moveInput = Vector2.zero;
            return;
        }

        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            Vector2 input = context.ReadValue<Vector2>();

            // 대각선 입력 방지: 한 축만 허용
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                moveInput = new Vector2(input.x, 0f).normalized;
            }
            else
            {
                moveInput = new Vector2(0f, input.y).normalized;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }
}
