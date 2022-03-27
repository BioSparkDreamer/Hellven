using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Fade Screen Variables")]
    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadeFromBlack, fadeToBlack;

    [Header("Game Over Screen")]
    public GameObject gameOverScreen;
    public bool isDead;
    public GameObject restartButton;

    [Header("Health Variables")]
    public Slider healthSlider;
    public TMP_Text healthText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        FadeFromBlack();
        UpdateHealthUI();
    }

    void Update()
    {
        //Fading the fade screen to black
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void UpdateHealthUI()
    {
        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = PlayerHealthController.instance.currentHealth;

        healthText.text = "Health: " + PlayerHealthController.instance.currentHealth.ToString() +
        "/" + PlayerHealthController.instance.maxHealth.ToString();
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        isDead = true;
        PauseMenu.instance.canPause = false;
        PlayerController.instance.theSR.enabled = false;
        PlayerController.instance.theRB.velocity = Vector3.zero;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }

    //Function to fade the fade screen from being black
    public void FadeFromBlack()
    {
        fadeFromBlack = true;
        fadeToBlack = false;
    }

    //Function to fade the fade screen to black
    public void FadeToBlack()
    {
        fadeToBlack = true;
        fadeFromBlack = false;
    }
}
