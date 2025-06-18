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

   void SetIdle(bool isIdle)
   {
      anim.SetBool("IsIdle", isIdle);
   }

   void SetWalk(bool isWalk)
   {
      anim.SetBool("IsWalk", isWalk);
   }

   void SetBackWalk(bool isBackWalk)
   {
      anim.SetBool("IsBackWalk", isBackWalk);
   }

   void SetRightWalk(bool isRightWalk)
   {
      anim.SetBool("IsRightWalk", isRightWalk);
   }

   void SetLeftWalk(bool isLeftWalk)
   {
      anim.SetBool("IsLeftWalk", isLeftWalk);
   }

   void SetBackIdle(bool isBackIdle)
   {
      anim.SetBool("IsBackIdle", isBackIdle);
   }

   void SetRightIdle(bool isRightIdle)
   {
      anim.SetBool("IsRightIdle", isRightIdle);
   }
   
   void SetLeftIdle(bool isLeftIdle)
   {
      anim.SetBool("IsLeftIdle", isLeftIdle);
   }

   void TriggerAttack1()
   {
      anim.SetTrigger("Attack1");
   }

   void TriggerAttack2()
   {
      anim.SetTrigger("Attack2");
   }

   void TriggerAttack3()
   {
      anim.SetTrigger("Attack3");
   }









}
