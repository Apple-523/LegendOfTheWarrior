
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputSystem inputControl;
    private PlayerAnimation playerAnimation;
    public Vector2 inputDirection;
    private PhyciseCheck phycisCheck;

    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;


    [Header("基本参数")]
    public float speed = 260;
    public float jumpForce = 16;
    private Rigidbody2D rigidbody2d;
    private CapsuleCollider2D capsuleCollider2D;
    [Header("受伤参数")]
    public bool isHurt;
    public float hurtForce;

    public bool isDead;
    public bool isAttack;
    public int attackCombo;



    #region 生命周期
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        inputControl = new PlayerInputSystem();
        rigidbody2d = GetComponent<Rigidbody2D>();
        phycisCheck = GetComponent<PhyciseCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        attackCombo = 0;

    }
    private void OnEnable()
    {
        inputControl.Enable();
        // 监听按下按键跳跃
        inputControl.GamePlay.Jump.started += onReceiveGamePlayJump;
        // 监听按下统计
        inputControl.GamePlay.Attack.started += onReceiveGameAttack;
    }


    private void OnDisable()
    {
        inputControl.Disable();
        inputControl.GamePlay.Jump.started -= onReceiveGamePlayJump;
        inputControl.GamePlay.Attack.started -= onReceiveGameAttack;
    }

    #endregion


    private void onReceiveGamePlayJump(InputAction.CallbackContext context)
    {
        if (phycisCheck.isOnGround)
        {
            // 给一个向上的冲力
            rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void onReceiveGameAttack(InputAction.CallbackContext context)
    {
        if (!phycisCheck.isOnGround)
        {
            return;
        }
        isAttack = true;
        playerAnimation.AnimAttack();
        attackCombo++;
        if (attackCombo >= 3)
        {
            attackCombo = attackCombo % 3 + 1;
        }
    }

    private void Update()
    {
        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead && !isAttack)
        {
            // 固定时钟频率
            Move();
        }

    }

    private void Move()
    {
        // 获取到input的数值
        Vector2 inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        rigidbody2d.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rigidbody2d.velocity.y);
        // 人物翻转
        float scaleX = Mathf.Abs(transform.localScale.x);
        if (inputDirection.x < 0)
        {
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        }
        // !!! 这里不能承接else 因为包含了0的情况，因此会自动右转
        if (inputDirection.x > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }



    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        // 受伤时，速度停下
        rigidbody2d.velocity = Vector2.zero;
        // 判断左方还是右方，并归一化
        Vector2 direction = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rigidbody2d.AddForce(direction * hurtForce, ForceMode2D.Impulse);
    }

    public void GetDeath()
    {
        if (!isDead)
        {
            isDead = true;
            inputControl.GamePlay.Disable();
        }

    }

    private void CheckState()
    {
        if (phycisCheck.isOnGround)
        {
            if (capsuleCollider2D.sharedMaterial != normal)
                capsuleCollider2D.sharedMaterial = normal;
        }
        else
        {
            if (capsuleCollider2D.sharedMaterial != wall)
            {
                capsuleCollider2D.sharedMaterial = wall;
            }

        }
    }
}
