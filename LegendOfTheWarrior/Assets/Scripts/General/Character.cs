using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    [Header("受伤无敌")]
    public float kInvulnerableDuration;
    private float invulnerableCounter;
    public bool isInvulnerable;
    [Header("收到伤害后的动作")]
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent onEventDead;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(name+"刚开始当前血量为"+currentHealth);
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
        Debug.Log("是谁伤害"+other.name);
        Debug.Log("受伤前当前血量为"+currentHealth);
        currentHealth -= other.damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // 触发死亡
            onEventDead?.Invoke();
        }
        else
        {
            // 受伤后有短暂的无敌状态
            TriggerInvulnerable();
            Debug.Log(name+"受伤当前血量为"+currentHealth);
            OnTakeDamage?.Invoke(other.transform);
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
