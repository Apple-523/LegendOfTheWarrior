
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputSystem inputControl;
    public Vector2 inputDirection;
    public float speed = 260;
    private Rigidbody2D rigidbody2d;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        inputControl = new PlayerInputSystem();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
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
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        rigidbody2d.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rigidbody2d.velocity.y);
        // 人物翻转

        if (inputDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


    }

}
