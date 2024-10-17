using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidbody2d;
    [HideInInspector] public Animator animator;

    [HideInInspector] public PhyciseCheck phyciseCheck;


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
    public bool isWaiting;

    public string kAnimWalk = "Walk";
    public string kAnimHurt = "Hurt";

    public string kAnimIsDead = "isDead";

    [Header("状态")]
    public bool isHurt;

    public bool isDead;

    /// <summary>
    /// 巡逻状态
    /// </summary>
    protected BaseState walkAroundState;

    /// <summary>
    /// 追击状态
    /// </summary>
    protected BaseState chaseState;
    private BaseState currentState;

    [Header("玩家检测")]
    public Vector2 centerOffect;
    public Vector2 checkBoxSize;
    public float checkDistance;
    public LayerMask checkPlayerLayerMask;

    protected virtual void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        phyciseCheck = GetComponent<PhyciseCheck>();
        currentSpeed = normalSpeed;
        currentTouchWallWaitTime = touchWallWaitTime;

    }

    private void OnEnable()
    {
        currentState = walkAroundState;
        currentState.OnEnter(this);
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }
    private void Update()
    {
        faceDirector = new Vector3(-transform.localScale.x, 0, 0);

        currentState.LogicUpdate();
        WallTouchWaitingTimeCounter();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead && !isWaiting)
        {
            Move();
        }
        currentState.PhysicsUpdate();
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

    public bool FoundPlayer()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(transform.position + (Vector3)centerOffect, checkBoxSize, 0, faceDirector,
        checkDistance, checkPlayerLayerMask);
        return raycastHit2D;
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.WalkAround => walkAroundState,
            NPCState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }


    #region 事件执行    

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

    public void OnTakeDeath()
    {
        gameObject.layer = 2;
        isDead = true;
        animator.SetBool(kAnimIsDead, true);
    }

    public void DestoryAfterAnimation()
    {
        Debug.Log("DestoryAfterAnimation");
        Destroy(this.gameObject);
    }
    #endregion


    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector3)centerOffect + transform.position, 0.2f);
    }


}
