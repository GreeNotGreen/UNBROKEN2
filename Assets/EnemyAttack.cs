using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackArea;
    public GameObject attackTrigger;
    public AI_Chase2 aiChaseScript; // 引用 AI_Chase2 脚本

    public float timeBeforeAttack = 1f; // 攻击前等待时间
    public float attackDuration = 0.5f; // 攻击持续时间
    public float attackCooldown = 1f; // 攻击冷却时间

    private bool isAttacking = false;
    private bool isOnCooldown = false;

    private void Start()
    {
        // 确保攻击区域初始处于关闭状态
        attackArea.SetActive(false);
    }

    private void Update()
    {
        // 如果正在攻击中，则更新攻击持续时间
        if (isAttacking)
        {
            attackDuration -= Time.deltaTime;
            // 如果攻击持续时间结束，则停止攻击
            if (attackDuration <= 0f)
            {
                StopAttack();
            }
        }

        // 如果处于冷却中，则减少冷却时间
        if (isOnCooldown)
        {
            attackCooldown -= Time.deltaTime;
            // 如果冷却时间结束，则停止冷却
            if (attackCooldown <= 0f)
            {
                isOnCooldown = false;
                // 重置触发器状态
                attackTrigger.GetComponent<AttackTrigger>().OnTrigger = false;
            }
        }

        // 只有在攻击不在冷却状态时才能启动攻击
        if (attackTrigger.GetComponent<AttackTrigger>().OnTrigger && !isAttacking && !isOnCooldown)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        // 禁用 AI 追踪行为
        if (aiChaseScript != null)
        {
            aiChaseScript.enabled = false;
        }
        else
        {
            Debug.LogError("AI_Chase2 脚本未附加到敌人对象上！");
        }

        // 等待攻击前的延迟时间
        yield return new WaitForSeconds(timeBeforeAttack);
        // 开始攻击
        StartAttack();
    }

    private void StartAttack()
    {
        // 激活攻击区域
        attackArea.SetActive(true);
        // 重置攻击持续时间
        attackDuration = 1f;
        isAttacking = true;
    }

    private void StopAttack()
    {
        // 关闭攻击区域
        attackArea.SetActive(false);
        // 启用 AI 追踪行为
        if (aiChaseScript != null)
        {
            aiChaseScript.enabled = true;
        }
        else
        {
            Debug.LogError("AI_Chase2 脚本未附加到敌人对象上！");
        }

        // 开始攻击冷却
        StartCooldown();
    }

    private void StartCooldown()
    {
        isAttacking = false;
        isOnCooldown = true;
        attackCooldown = 1f; // 重置冷却时间
    }
}
