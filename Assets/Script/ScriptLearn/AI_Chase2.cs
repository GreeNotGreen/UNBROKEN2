using UnityEngine;

public class AI_Chase2 : MonoBehaviour
{
    public GameObject player; // 玩家对象
    public float speed = 3f; // 追踪速度
    public float distanceBetween = 5f; // 与玩家保持的距离
    public float randomWalkInterval = 2f; // 随机走动间隔
    private float lastRandomWalkTime; // 上次随机走动时间
    private Vector2 randomDirection; // 随机移动方向
    [SerializeField]   private SpriteRenderer spriteRenderer; // 精灵渲染器

    void Start()
    {
        lastRandomWalkTime = Time.time;
        randomDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance > distanceBetween)
            {
                // 检查是否达到随机走动的条件
                if (Time.time - lastRandomWalkTime > randomWalkInterval)
                {
                    // 重新生成随机移动方向
                    randomDirection = Random.insideUnitCircle.normalized;

                    // 更新上次随机走动时间
                    lastRandomWalkTime = Time.time;
                }

                // 使用随机移动方向进行移动
                transform.position += new Vector3(randomDirection.x, randomDirection.y, 0) * speed / 2 * Time.deltaTime;

                // 根据随机移动方向翻转精灵的朝向
                if (randomDirection.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (randomDirection.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                // 如果距离大于 distanceBetween，就朝玩家移动
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                // 根据玩家位置翻转精灵的朝向
                if (player.transform.position.x < transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }

            }
        }
    }
}
