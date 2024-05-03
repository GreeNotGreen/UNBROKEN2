using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackArea;
    public GameObject attackTrigger;
    public AI_Chase2 aiChaseScript; // 引用 AI_Chase2 脚本
    private GameObject player; // 引用 AI_Chase2 脚本中的玩家对象

    public float timeBeforeAttack = 1f; // 攻击前等待时间
    public float SetattackDuration = 0.5f; // Set攻击持续时间
    public float attackCooldown = 1f; // 攻击冷却时间

    private float attackDuration = 0f; // 攻击持续时间
    private bool isAttacking = false;
    private bool isOnCooldown = false;

    //for rush on player
    public bool canRush = false;
    public float rushSpeed = 5f;
    private Vector3 playerPositionOnRush;

    private void Start()
    {
        // 确保攻击区域初始处于关闭状态
        attackArea.SetActive(false);
        // 获取 AI_Chase2 脚本中的玩家对象
        if (aiChaseScript != null)
        {
            player = aiChaseScript.player;
        }
        attackDuration = SetattackDuration;
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
            //get player position
            playerPositionOnRush = player.transform.position;
            Debug.Log("player position get");

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
        attackDuration = SetattackDuration;
        isAttacking = true;

        if (canRush && player != null)
        {
            // 直接冲向玩家
            transform.position = Vector3.MoveTowards(transform.position, playerPositionOnRush, rushSpeed *10* Time.deltaTime);
        }
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
