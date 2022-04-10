using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;

    [Header("Game Over Menu Varibales")]
    public GameObject[] buttons;
    public GameObject gameOverScreen, restartButton;
    public CanvasGroup gameOverMenu, controlsMenu, creditsMenu;
    public string loadToMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeActiveButtons(int buttonToChoose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChoose]);
    }

    public void ShowGameOverScreen()
    {
        //Show the Game Over Screen
        gameOverScreen.SetActive(true);
        OpenGameOverMenu();
        Time.timeScale = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }

    //Function for turning off start screen and opening the main menu on by setting alpha to 1
    public void OpenGameOverMenu()
    {
        gameOverMenu.alpha = 1;
        gameOverMenu.blocksRaycasts = true;
    }

    //Function for closing the main menu by setting alpha to 0 and allowing for clicks to not be made
    public void CloseGameOverMenu()
    {
        gameOverMenu.alpha = 0;
        gameOverMenu.blocksRaycasts = false;
    }

    //Function for opening the controls menu by setting alpha to 1 and allowing for clicks to be made
    public void OpenControls()
    {
        controlsMenu.alpha = 1;
        controlsMenu.blocksRaycasts = true;
    }

    //Function for closing the controls menu by setting alpha to 0 and allowing for clicks to not be made
    public void CloseControls()
    {
        controlsMenu.alpha = 0;
        controlsMenu.blocksRaycasts = false;
    }

    //Function for credits the options menu by setting alpha to 0 and allowing for clicks to not be made
    public void OpenCredits()
    {
        creditsMenu.alpha = 1;
        creditsMenu.blocksRaycasts = true;
    }

    //Function for credits the options menu by setting alpha to 0 and allowing for clicks to not be made
    public void CloseCredits()
    {
        creditsMenu.alpha = 0;
        creditsMenu.blocksRaycasts = false;
    }

    public void RestartGame()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Function for quitting out of the game
    public void LoadMainMenu()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(loadToMenu);
    }

    public void ChangeTimeScale()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void PlayButtonPress()
    {
        AudioManager.instance.PlaySFX(13);
    }
}
