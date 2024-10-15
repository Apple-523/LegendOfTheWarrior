
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputSystem inputControl;
    public Vector2 inputDirection;
    private PhyciseCheck phycisCheck;
    [Header("基本参数")]
    public float speed = 260;
    public float jumpForce = 16;
    private Rigidbody2D rigidbody2d;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        inputControl = new PlayerInputSystem();
        rigidbody2d = GetComponent<Rigidbody2D>();
        phycisCheck = GetComponent<PhyciseCheck>();

    }
    private void OnEnable()
    {
        inputControl.Enable();
        // 监听按下按键跳跃
        inputControl.GamePlay.Jump.started += onReceiveGamePlayJump;
    }
    private void OnDisable()
    {
        inputControl.Disable();
        inputControl.GamePlay.Jump.started -= onReceiveGamePlayJump;
    }

    private void onReceiveGamePlayJump(InputAction.CallbackContext context)
    {
        if (phycisCheck.isOnGround)
        {
            // 给一个向上的冲力
            rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void Update()
    {


    }
    private void FixedUpdate()
    {
        // 固定时钟频率
        Move();
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
        else
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
