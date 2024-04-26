using System.Collections;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    public bool _OnHit = false;
    private bool hasExecuted = false;
    public GameObject attackArea;
    public KeyCode attackKey = KeyCode.Space;
    public int attackButton = 0;

    public bool attacking = false;
    public float timeToAttack = 0.25f;
    private float timer = 0f;

    //knockback
    public float knockbackForce = 10f;

    // Additional attack cooldown
    public float additionalCooldown = 0.5f;
    private bool isAdditionalCooldown = false;

    void Update()
    {
        if (Input.GetKeyDown(attackKey) || Input.GetMouseButtonDown(attackButton))
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer > timeToAttack)
            {
                attacking = false;
                timer = 0f;
                attackArea.SetActive(false);
                hasExecuted = false; // 重置为false

                // Start additional cooldown
                StartCoroutine(AdditionalCooldown());
            }
        }

    }

    public void Attack()
    {
        if (!attacking && !isAdditionalCooldown)
        {
            attacking = true;
            attackArea.SetActive(true);
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(timeToAttack);
        attacking = false;
        attackArea.SetActive(false);
    }

    IEnumerator AdditionalCooldown()
    {
        isAdditionalCooldown = true;
        yield return new WaitForSeconds(additionalCooldown);
        isAdditionalCooldown = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && attacking && !hasExecuted)
        {
            _OnHit = true;
            Debug.Log("Onhit");
            hasExecuted = true;
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                // 计算击退方向
                Vector2 direction = (other.transform.position - transform.position).normalized;
                // 施加较小的击退力量，然后作用时间较短
                enemyRigidbody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                StartCoroutine(StopKnockback(enemyRigidbody));
            }
        }
    }

    IEnumerator StopKnockback(Rigidbody2D rigidbody)
    {
        yield return new WaitForSeconds(0.1f);
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
