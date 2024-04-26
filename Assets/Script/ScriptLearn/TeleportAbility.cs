using System.Collections;
using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] KeyCode teleportKey = KeyCode.Return; // 触发瞬移的按键
    [SerializeField] int attackButton = 1; // 1=右键
    [SerializeField] float teleportOffset = 1f; // 瞬移偏移量，用于控制与敌人的距离
    [SerializeField] float maxDistanceForTeleport = 100f; // 允许瞬移的最大距离

    [SerializeField] PlayerAttack2 playerAttack;
    [SerializeField] SkillCD skillCD;
    public bool isTeleporting = false;

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack2>(); // 获取 PlayerAttack2 脚本的引用
    }

    void Update()
    {
        if (!isTeleporting && (Input.GetKeyDown(teleportKey) || Input.GetMouseButtonDown(attackButton)) && skillCD.CanUseSkill())
        {

            skillCD.Skilled = true;
            TeleportToNearestEnemy();
        }
    }

    void TeleportToNearestEnemy()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            StartCoroutine(ResetTeleportingState());
            float distanceToNearestEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            if (distanceToNearestEnemy <= maxDistanceForTeleport)
            {
                FindObjectOfType<HitStop>().Stop();

                Vector3 teleportPosition = GetTeleportPosition(nearestEnemy.transform.position, transform.position);
                transform.position = teleportPosition;

                // 调用 PlayerAttack2 脚本中的 Attack() 方法 && canskill
                if (playerAttack != null)
                {
                    isTeleporting = true;
                    playerAttack.Attack();
                }
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    Vector3 GetTeleportPosition(Vector3 enemyPosition, Vector3 playerPosition)
    {
        // 计算瞬移位置，将玩家瞬移到距离敌人一定距离的位置
        Vector3 directionToEnemy = (enemyPosition - playerPosition).normalized;
        Vector3 teleportPosition = enemyPosition - directionToEnemy * teleportOffset;
        return teleportPosition;
    }

    IEnumerator ResetTeleportingState()
    {
        yield return new WaitForSeconds(0.05f); // 0.05秒后重置 isTeleporting
        isTeleporting = false;
    }
}
