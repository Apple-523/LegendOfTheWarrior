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

    private PhyciseCheck phyciseCheck;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        phyciseCheck = GetComponent<PhyciseCheck>();
    }
    private void Update()
    {
        animator.SetFloat(kAnimVelocityX, Mathf.Abs(rigidbody2d.velocity.x));
        animator.SetFloat(kAnimVelocityY, rigidbody2d.velocity.y);
        animator.SetBool(kAnimIsOnGround,phyciseCheck.isOnGround);
    }
}
