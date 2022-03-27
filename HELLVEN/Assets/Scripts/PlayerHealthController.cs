using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Player Health Variables")]
    public int maxHealth;
    [HideInInspector]
    public int currentHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentHealth = maxHealth;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;
        AudioManager.instance.PlaySFXAdjusted(3);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            AudioManager.instance.PlaySFXAdjusted(2);
            UIController.instance.ShowGameOver();
        }

        UIController.instance.UpdateHealthUI();
    }
}
