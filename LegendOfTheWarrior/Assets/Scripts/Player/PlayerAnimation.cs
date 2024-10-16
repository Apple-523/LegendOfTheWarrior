using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    public static string kAnimVelocityX = "velocityX";
    public static string kAnimVelocityY = "velocityY";
    public static string kAnimIsOnGround = "isOnGround";
    public static string kAnimIsDead = "isDead";
    public static string kAnimIsAttack = "isAttack";
    public static string kAnimHurtTrigger = "hurtTrigger";

    public static string kAnimAttackCombo = "attackCombo";
    public static string kAnimAttackTrig = "AttackTrig";



    private PhyciseCheck phyciseCheck;
    private PlayerController playerController;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        phyciseCheck = GetComponent<PhyciseCheck>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        animator.SetFloat(kAnimVelocityX, Mathf.Abs(rigidbody2d.velocity.x));
        animator.SetFloat(kAnimVelocityY, rigidbody2d.velocity.y);
        animator.SetBool(kAnimIsOnGround, phyciseCheck.isOnGround);
        animator.SetBool(kAnimIsDead, playerController.isDead);
        animator.SetBool(kAnimIsAttack, playerController.isAttack);
        animator.SetInteger(kAnimAttackCombo, playerController.attackCombo);
    }

    public void PlayerHurt()
    {
        animator.SetTrigger(kAnimHurtTrigger);
    }

    public void AnimAttack()
    {
        Debug.Log("AnimAttack");
        animator.SetTrigger(kAnimAttackTrig);
    }
}
