using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigidbody2d;
    protected Animator animator;

    PhyciseCheck phyciseCheck;


    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;

    public float currentSpeed;

    public Vector3 faceDirector;

    public float hurtForce;

    public Transform attacker;
    [Header("计时器")]
    public float touchWallWaitTime = 2;
    private float currentTouchWallWaitTime = 0;
    private bool isWaiting;

    private string kAnimWalk = "Walk";
    private string kAnimHurt = "Hurt";

    private string kAnimIsDead = "isDead";

    [Header("状态")]
    public bool isHurt;

    public bool isDead;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        phyciseCheck = GetComponent<PhyciseCheck>();
        currentSpeed = normalSpeed;
        currentTouchWallWaitTime = touchWallWaitTime;

    }

    private void Update()
    {
        faceDirector = new Vector3(-transform.localScale.x, 0, 0);

        if (phyciseCheck.isTouchLeftWall && faceDirector.x < 0 || // 撞左边墙时，脸要朝左侧
        phyciseCheck.isTouchRightWall && faceDirector.x > 0)
        {
            isWaiting = true;
            animator.SetBool(kAnimWalk, false);
        }
        WallTouchWaitingTimeCounter();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead)
        {
            Move();
        }

    }

    public virtual void Move()
    {
        rigidbody2d.velocity = new Vector2(currentSpeed * faceDirector.x * Time.deltaTime, faceDirector.y);
    }

    public void WallTouchWaitingTimeCounter()
    {
        if (isWaiting)
        {
            currentTouchWallWaitTime -= Time.deltaTime;
            if (currentTouchWallWaitTime <= 0)
            {
                isWaiting = false;
                animator.SetBool(kAnimWalk, true);
                currentTouchWallWaitTime = touchWallWaitTime;
                // 到时间了转身
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                faceDirector = -faceDirector;
            }
        }
    }

    public void OnTakeDamage(Transform attackTransform)
    {
        attacker = attackTransform;
        // 转身
        Debug.Log("转身");
        if (attackTransform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attackTransform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // 受伤被击退
        isHurt = true;
        animator.SetTrigger(kAnimHurt);
        Vector2 dir = new Vector2(transform.position.x - attackTransform.position.x, 0).normalized;
        rigidbody2d.AddForce(dir * hurtForce, ForceMode2D.Impulse);

    }

    public void OnTakeDeath() {
        isDead = true;
        animator.SetBool(kAnimIsDead, true);
        animator.SetTrigger("DeadTrig");
    }
 }
