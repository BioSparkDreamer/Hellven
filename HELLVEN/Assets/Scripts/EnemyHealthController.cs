using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [Header("Health Variables")]
    public int enemyHealth;
    public bool isAngel, isDemon, isFlyer;

    public void TakeDamage(int damageToTake)
    {
        enemyHealth -= damageToTake;

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            AudioManager.instance.PlaySFXAdjusted(2);
            Destroy(gameObject);
        }
    }
}
