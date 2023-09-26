using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    // 最大血量
    public float maxHealth;
    // 当前血量
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(Attack attacker)
    {
        Debug.Log(attacker.damage);
        currentHealth -= attacker.damage;
    }

}
