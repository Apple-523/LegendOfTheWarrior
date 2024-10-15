using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    [Header("受伤无敌")]
    public float kInvulnerableDuration;
    private float invulnerableCounter;
    public bool isInvulnerable;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isInvulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                isInvulnerable = false;
            }
        }
    }

    /// <summary>
    /// 接收伤害
    /// </summary>
    /// <param name="other"></param>
    public void TakeDamage(Attack other)
    {
        if (isInvulnerable)
        {
            return;
        }
        currentHealth -= other.damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //TODO: wmy 触发死亡
        }
        else
        {
            // 受伤后有短暂的无敌状态
            TriggerInvulnerable();
        }

    }
    private void TriggerInvulnerable()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            invulnerableCounter = kInvulnerableDuration;
        }
    }
}
