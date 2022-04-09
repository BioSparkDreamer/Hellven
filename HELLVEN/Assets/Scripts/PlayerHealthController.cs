using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Player Health Variables")]
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    [Header("Invincibility Variables")]
    public float invincibleLength;
    private float invincibleCounter;
    public float flashLength;
    private float flashCounter;
    public SpriteRenderer[] playerSprites;
    public Color hurtColor, defaultColor;

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
        //Get the default color of each sprite renderer and set the variable equal to its color
        foreach (SpriteRenderer sr in playerSprites)
            defaultColor = sr.color;
    }

    void Update()
    {
        //Doing invinciblity Stuff
        if (invincibleCounter > 0)
        {
            //Make the timers count down
            invincibleCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            //If the flash counter reaches zero then turn the sprite on and off
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                    sr.color = hurtColor;
                }

                //Reset the flash counter
                flashCounter = flashLength;
            }

            //If the invincibility counter reaches zero then turn the sprites on
            if (invincibleCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                    sr.color = defaultColor;
                }

                flashCounter = 0f;
            }
        }
    }

    public void TakeDamage(int damageToTake)
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= damageToTake;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.GameOver();
                AudioManager.instance.PlaySFXAdjusted(2);
            }
            else
            {
                invincibleCounter = invincibleLength;
                AudioManager.instance.PlaySFXAdjusted(3);
            }

            UIController.instance.UpdateHealthUI();
        }
    }
}
