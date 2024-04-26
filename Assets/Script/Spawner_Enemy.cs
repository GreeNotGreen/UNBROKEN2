using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人预制体
    public Transform playerTransform; // 玩家对象的Transform
    public float spawnInterval = 2f; // 生成间隔时间
    public float spawnRadius = 3f; // 生成范围半径

    void Start()
    {
        // 开始生成敌人
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // 生成敌人
            SpawnEnemy();

            // 等待一定时间后再生成下一个敌人
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // 生成敌人的位置在生成器位置附近随机
        Vector2 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

        // 实例化敌人
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 获取敌人脚本
        AI_Chase2 enemyScript = newEnemy.GetComponent<AI_Chase2>();

        // 分配玩家对象给敌人脚本
        if (enemyScript != null && playerTransform != null)
        {
            enemyScript.player = playerTransform.gameObject;
        }
        else
        {
            Debug.LogWarning("PlayerTransform or AI_Chase2 script is missing!");
        }
    }

    void OnDrawGizmosSelected()
    {
        // 绘制生成范围的可视化辅助线
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
