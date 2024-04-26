using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 0;
    public int _maxHealth = 0;
    [SerializeField] private Text healthUI;
    public SpriteRenderer spriteRenderer; // 在 Unity 编辑器中拖拽 SpriteRenderer 组件到这个字段中

    public bool isInvulnerable = false; // 标记是否无敌
    public float invulnerabilityDuration = 1f; // 无敌持续时间

    private Color originalColor; // 原始颜色
    private bool isBlinking = false; // 是否正在闪烁

    // Start is called before the first frame update
    void Start()
    {
        originalColor = spriteRenderer.color; // 保存原始颜色
    }

    // Update is called once per frame
    void Update()
    {
        if (healthUI != null)
        {
            healthUI.text = "hp: " + health.ToString();
        }

        // 如果处于无敌状态且未在闪烁，则开始闪烁
        if (isInvulnerable && !isBlinking)
        {
            StartCoroutine(BlinkRoutine());
        }
    }

    IEnumerator BlinkRoutine()
    {
        isBlinking = true; // 标记为正在闪烁

        float minAlpha = 0.3f;
        float maxAlpha = 1f;
        float alphaChangeSpeed = 3f; // 透明度变化速度

        while (isInvulnerable)
        {
            // 计算当前透明度
            float alpha = Mathf.PingPong(Time.time * alphaChangeSpeed, maxAlpha - minAlpha) + minAlpha;

            // 创建新的颜色，只改变透明度
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            spriteRenderer.color = newColor;

            yield return null;
        }

        // 闪烁结束，恢复原始颜色
        spriteRenderer.color = originalColor;

        // 结束闪烁
        isBlinking = false;
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        if (!isInvulnerable) // 如果不是无敌状态
        {
            this.health -= amount;

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(InvulnerabilityRoutine()); // 启动无敌协程
            }
        }
    }

    IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true; // 设置为无敌状态

        // 等待无敌持续时间
        yield return new WaitForSeconds(invulnerabilityDuration);

        // 取消无敌状态
        isInvulnerable = false;
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        }

        bool wouldBeOverMaxHealth = health + amount > _maxHealth;

        if (wouldBeOverMaxHealth)
        {
            this.health = _maxHealth;
        }
        else
        {
            this.health += amount;
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
