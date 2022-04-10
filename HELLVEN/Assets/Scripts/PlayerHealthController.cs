using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Player Health Variables")]
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    public GameObject deathEffect, ballDeathEffect;

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
            GameManager.instance.healthLost += damageToTake;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                ShowDeathEffect();
                GameManager.instance.GameOver();
                AudioManager.instance.PlaySFXAdjusted(9);
            }
            else
            {
                invincibleCounter = invincibleLength;
                AudioManager.instance.PlaySFXAdjusted(11);
            }

            UIController.instance.UpdateHealthUI();
        }
    }

    public void AddHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthUI();
    }

    public void ShowDeathEffect()
    {
        //Show Death Effect Depending on what mode the player is currently in
        if (PlayerController.instance.standingObject.activeSelf)
        {
            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, transform.rotation);
        }
        else if (PlayerController.instance.ballObject.activeSelf)
        {
            //Instantiate the Death Effect at the Bomb Point position to match the height of the sprite
            if (ballDeathEffect != null)
                Instantiate(ballDeathEffect, PlayerController.instance.bombPoint.position, transform.rotation);
        }
    }
}
