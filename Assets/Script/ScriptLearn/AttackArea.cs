using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 1;
    public string _Target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null && collision.CompareTag(_Target))
        {
            Health health = collision.GetComponent<Health>();
            health.Damage(damage);
        }
    }
}