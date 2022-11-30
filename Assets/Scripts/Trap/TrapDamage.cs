using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{

    [SerializeField] private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hi");
            var atm = collision.collider.GetComponent<Health>();
            if (atm != null)
            {
                atm.TakeDamage(damage);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            //DealDamageEnemy(target);

            var enemy = collision.collider.GetComponent<Enemy_Shoot>();
            {
                Debug.Log("Enemy took damage");
                if (enemy != null)
                {
                    enemy.EnemyTakeDamage(damage);
                }
            }
        }
    }

}
