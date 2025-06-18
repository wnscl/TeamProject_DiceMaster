using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
   private Animator anim;

   private void Awake()
   {
      anim = GetComponent<Animator>();
   }



  public void SetMove(bool isMove)
   {
      anim.SetBool("IsMove", isMove);
   }










}
