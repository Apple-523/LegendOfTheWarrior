using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PhyciseCheck : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider2D;
    [Header("检测参数")]
    /// <summary>
    /// 检测地面的半径
    /// </summary>
    public float checkRadius = 0.2f;

    public bool needAutoCheck;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public LayerMask groundLayer;
    [Header("状态")]
    /// <summary>
    /// 是否在地面
    /// </summary>
    public bool isOnGround;
    public bool isTouchLeftWall;
    public bool isTouchRightWall;

    private void Awake() {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if (needAutoCheck) {
            rightOffset = new Vector2(capsuleCollider2D.bounds.size.x/2 + capsuleCollider2D.offset.x,
            capsuleCollider2D.bounds.size.y/2);

            leftOffset = new Vector2(-capsuleCollider2D.bounds.size.x/2 + capsuleCollider2D.offset.x,
            capsuleCollider2D.bounds.size.y/2);
        }
    }
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        // - 检测是否在地面
        isOnGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        // - 检测是否在地面
        isTouchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        // - 检测是否在地面
        isTouchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }
    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
     Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRadius);   
     Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset,checkRadius);   
     Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset,checkRadius);   
    }
}
