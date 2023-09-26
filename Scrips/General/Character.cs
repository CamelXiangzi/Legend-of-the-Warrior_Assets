using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Unity自带的事件
using UnityEngine.Events;

public class Character : MonoBehaviour
{

    [Header("基本属性")]
    // 最大血量
    public float maxHealth;
    // 当前血量
    public float currentHealth;

    [Header("受伤无敌")]
    // 计算无敌的时间
    public float invulnerableDuration;
    // 计时器
    private float invulnerableCounter;
    // 状态
    public bool invulnerable;

    // 受伤的事件
    // Transform  传入的“变量”
    public UnityEvent<Transform> OnTakeDamage;
    // 死亡事件
    public UnityEvent OnDie;

    private void Awake()
    {

    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // 计时器
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            // 退出无敌状态
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;

            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        // 如果不是无敌状态，则进行血量计算
        if (!invulnerable)
        {
            // Debug.Log(attacker.damage);

            if (currentHealth - attacker.damage > 0)
            {
                currentHealth -= attacker.damage;
                // 受伤后进入无敌状态
                TriggerInvulnerable();
                // 执行受伤
                if (OnTakeDamage != null)
                {
                    // 启动事件注册的所有方法
                    OnTakeDamage.Invoke(attacker.transform);
                }

            }
            else
            {
                // 当前血量不能扣到负数
                currentHealth = 0;
                // 触发死亡
                if (OnDie != null)
                {
                    // 启动事件注册的所有方法
                    OnDie.Invoke();
                }
            }
        }
    }

    private void TriggerInvulnerable()
    {
        // 如果不是无敌状态，则进入无敌状态
        if (!invulnerable)
        {
            invulnerable = true;
            // 无敌状态的持续时间
            invulnerableCounter = invulnerableDuration;
        }
    }

}
