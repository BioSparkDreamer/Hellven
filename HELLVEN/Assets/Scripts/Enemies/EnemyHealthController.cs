using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [Header("Health Variables")]
    public int enemyHealth;
    public bool isAngel, isDemon, isFlyer, isBlob;
    public GameObject deathEffect;
    public Slider healthSlider;

    [Header("Hurt Variables")]
    public float hurtTime;
    private float hurtCounter;
    private SpriteRenderer theSR;
    public Color defaultColor, hurtColor;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        defaultColor = theSR.color;
        healthSlider.maxValue = enemyHealth;
        UpdateHealthUI();
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

    public void UpdateHealthUI()
    {
        healthSlider.value = enemyHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        enemyHealth -= damageToTake;

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;

            if (isDemon || isAngel)
                AudioManager.instance.PlaySFX(4);
            else if (isFlyer)
                AudioManager.instance.PlaySFX(1);
            else if (isBlob)
                AudioManager.instance.PlaySFX(2);

            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, transform.rotation);

            GameManager.instance.enemiesKilled++;
            Destroy(transform.parent.gameObject);
        }
        else
        {
            hurtCounter = hurtTime;
        }

        UpdateHealthUI();
    }
}
