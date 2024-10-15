using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PhyciseCheck : MonoBehaviour
{
    [Header("检测参数")]
    /// <summary>
    /// 检测地面的半径
    /// </summary>
    public float checkRadius = 0.2f;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    [Header("状态")]
    /// <summary>
    /// 是否在地面
    /// </summary>
    public bool isOnGround;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        // - 检测是否在地面
        isOnGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
    }
    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
     Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRadius);   
    }
}
