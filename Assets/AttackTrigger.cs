using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public bool OnTrigger;
    public float knockbackForce = 0.1f;
    // Start is called before the first frame update

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrigger = true;
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
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTrigger = false;
    }

    private void Update()
    {
        if (OnTrigger)
        {
            Debug.Log("Ontrigger");
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