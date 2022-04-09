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

    [Header("Energy Variables")]
    public Slider energySlider;
    public TMP_Text energyText;

    [Header("Shooting Variables")]
    public TMP_Text ammoText;
    public TMP_Text bombText;

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
        UpdateAmmoUI();
        UpdateBombUI();
    }

    void Update()
    {
        FadingScreen();
    }

    public void UpdateHealthUI()
    {
        energySlider.maxValue = PlayerHealthController.instance.maxHealth;
        energySlider.value = PlayerHealthController.instance.currentHealth;

        energyText.text = "Energy:" + PlayerHealthController.instance.currentHealth.ToString() +
        "/" + PlayerHealthController.instance.maxHealth.ToString();
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = PlayerController.instance.currentAmmo.ToString("F0");
    }

    public void UpdateBombUI()
    {
        bombText.text = PlayerController.instance.currentBombAmmo.ToString("F0");
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

    void FadingScreen()
    {
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
                fadeToBlack = false;
        }

        if (fadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
                fadeFromBlack = false;
        }
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
