using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public KeyCode attackKey = KeyCode.Space; // 瞬移的按键
    public int attackButton = 0; // 0=左键
    public float knockbackForce = 10f; // 击退力大小
    public bool _isAttacking = false;

    // 属性用来获取和设置_isAttacking字段的值
    public bool IsAttacking
    {
        get { return _isAttacking; }
        set { _isAttacking = value; }
    }

    void Update()
    {
        // 检测玩家是否按下空格键攻击
        if (Input.GetKeyDown(attackKey)|| Input.GetMouseButtonDown(attackButton))
        {
            Attack();
        }
    }

    void LateUpdate()
    {
        // 重置_isAttacking为false
        _isAttacking = false;
    }

    public void Attack()
    {
        // 发出射线检测是否有敌人在攻击范围内
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f);

        // 设置_isAttacking为true
        _isAttacking = true;

        // 如果射线检测到敌人
        if (hit.collider != null)
        {
            // 检查被攻击对象是否具有 "Enemy" 标签
            if (hit.collider.CompareTag("Enemy"))
            {
                // 获取敌人的 Rigidbody2D 组件
                Rigidbody2D enemyRigidbody = hit.collider.GetComponent<Rigidbody2D>();
                if (enemyRigidbody != null)
                {
                    // 计算击退方向
                    Vector2 direction = (hit.collider.transform.position - transform.position).normalized;
                    // 施加击退力
                    enemyRigidbody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
