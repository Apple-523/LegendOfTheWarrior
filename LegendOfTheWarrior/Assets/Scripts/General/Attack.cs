using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    [Header("攻击范围")]
    public float attackRange;
    [Header("攻击频率")]
    public float attackRate;

    private void OnTriggerStay2D(Collider2D other)
    {
        //TODO: 判断other是否为player
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
