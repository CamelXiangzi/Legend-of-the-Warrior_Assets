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
        // 判断 other 身上的 GetComponent<Character>() 是否有代码
        if (other.GetComponent<Character>() != null)
        {
            other.GetComponent<Character>().TakeDamage(this);

        }
    }
}

