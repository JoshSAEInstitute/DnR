using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{

    private Health death;

    private GameObject target;
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        death = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Hi");
            var atm = collision.gameObject.GetComponent<Health>();
            if (atm != null)
            {
                atm.TakeDamage(damage);
            }
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            //DealDamageEnemy(target);

            var enemy = collision.gameObject.GetComponent<Enemy_Shoot>();
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
