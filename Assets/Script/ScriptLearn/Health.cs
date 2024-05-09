using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//因为这个script 是 player跟 enemy 共用的    需要小心写player的时候 加上 forPlayerOnly

public class Health : MonoBehaviour
{
    [SerializeField] private bool forPlayerOnly = false;

    public int health = 0;
    public int _maxHealth = 0;
    [SerializeField] private Text healthUI;
    public SpriteRenderer spriteRenderer; // 在 Unity 编辑器中拖拽 SpriteRenderer 组件到这个字段中

    public bool isInvulnerable = false; // 标记是否无敌
    public float invulnerabilityDuration = 1f; // 无敌持续时间

    private Color originalColor; // 原始颜色
    private bool isBlinking = false; // 是否正在闪烁

    //for hurt screen && heal screen
    [SerializeField] private Image hurtScreen;
    [SerializeField] private Image healScreen;
    //for last chance
    public bool isLastChance = false;
    [SerializeField] private Image LastChanceScreen;

    //for blood particle
    [SerializeField] private ParticleSystem bloodParticle;


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

        if (forPlayerOnly)
        {
            if (isLastChance)
            {
                //last chance screen有特效
                LastChanceScreen.gameObject.SetActive(true);
            }
            //如果超过10 last chance = false;
            if (health >= 10)
            {
                isLastChance = false;
                LastChanceScreen.gameObject.SetActive(false);
            }
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
            if(bloodParticle != null)
                bloodParticle.Play();

            // player Use
            if (forPlayerOnly)
            {
                if (health <= 0 && !isLastChance)
                {
                    Debug.Log("islasthit = true");
                    //last chance (hp =1)
                    isLastChance = true;
                    health = 1;
                }
                else if (health <= 0 && isLastChance)
                {
                    Debug.Log("die");
                    Die();
                }
                else
                {
                    Debug.Log("hurt");
                    if (hurtScreen != null)
                    {
                        hurtScreen.gameObject.SetActive(true);
                        // 使用 LeanTween 补间 alpha 值从 1 到 0，在 1 秒内完成
                        LeanTween.value(hurtScreen.gameObject, 1f, 0f, 1f)
                            .setOnUpdate((float alpha) =>
                            {
                                // 在补间过程中更新 alpha 值
                                Color screenColor = hurtScreen.color;
                                screenColor.a = alpha;
                                hurtScreen.color = screenColor;
                            })
                            .setOnComplete(() =>
                            {
                                // 补间完成后禁用 hurtScreen
                                hurtScreen.gameObject.SetActive(false);
                            });
                    }
                    StartCoroutine(InvulnerabilityRoutine()); // 启动无敌协程
                }
            }

            //enemy Use
            else
            {
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

            Debug.Log("heal");
            if (healScreen != null)
            {
                healScreen.gameObject.SetActive(true);
                // 使用 LeanTween 补间 alpha 值从 1 到 0，在 1 秒内完成
                LeanTween.value(healScreen.gameObject, 1f, 0f, 1f)
                    .setOnUpdate((float alpha) =>
                    {
                        // 在补间过程中更新 alpha 值
                        Color screenColor = healScreen.color;
                        screenColor.a = alpha;
                        healScreen.color = screenColor;
                    })
                    .setOnComplete(() =>
                    {
                        // 补间完成后禁用 hurtScreen
                        healScreen.gameObject.SetActive(false);
                    });
            }
        }
        

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
