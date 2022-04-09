using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [Header("Health Variables")]
    public int enemyHealth;
    public bool isAngel, isDemon, isFlyer;
    public GameObject deathEffect;

    [Header("Hurt Variables")]
    public float hurtTime;
    private float hurtCounter;
    private SpriteRenderer theSR;
    public Color defaultColor, hurtColor;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        defaultColor = theSR.color;
    }

    void Update()
    {
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;
            theSR.color = hurtColor;

            if (hurtCounter <= 0)
                theSR.color = defaultColor;
        }
    }

    public void TakeDamage(int damageToTake)
    {
        enemyHealth -= damageToTake;

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            AudioManager.instance.PlaySFXAdjusted(2);

            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        else
        {
            hurtCounter = hurtTime;
        }
    }
}
