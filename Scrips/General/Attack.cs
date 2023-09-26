using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 伤害值
    public int damage;
    // 攻击范围
    public float attackRange;
    // 攻击频率
    public float attackRate;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>().TakeDamage(this);
    }
}

