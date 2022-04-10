using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [Header("Pausing Variables")]
    public GameObject pauseScreen;
    public GameObject resumeButton;
    public bool canPause = true;

    [Header("Loading to Main Menu")]
    public string loadToMenu;

    [Header("Pause Menu Variables")]
    public GameObject[] buttons;
    public CanvasGroup pauseMenu, controlsMenu, optionsMenu, creditsMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && canPause)
        {
            PauseUnPause();
        }
    }

    public void PauseUnPause()
    {
        if (!pauseScreen.activeInHierarchy)
        {
            //Turning on the pause menu and pausing the game
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            PlayerController.instance.canMove = false;
            OpenPauseMenu();

            //Setting the current selected button to the resume button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            PlayerController.instance.canMove = true;

            ClosePauseMenu();
            CloseControls();
            CloseOptions();
            CloseCredits();
        }

        AudioManager.instance.PlayPauseMusic();
    }

    //Function for switching the active button using the eventsystem using a parameter
    public void ChangeActiveButton(int buttonToChoose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChoose]);
    }

    public void ResumeGame()
    {
        PauseUnPause();
    }

    //Function for turning off start screen and opening the main menu on by setting alpha to 1
    public void OpenPauseMenu()
    {
        pauseMenu.alpha = 1;
        pauseMenu.blocksRaycasts = true;
    }

    //Function for closing the main menu by setting alpha to 0 and allowing for clicks to not be made
    public void ClosePauseMenu()
    {
        pauseMenu.alpha = 0;
        pauseMenu.blocksRaycasts = false;
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

    //Function for opening the options menu by setting alpha to 1 and allowing for clicks to be made
    public void OpenOptions()
    {
        optionsMenu.alpha = 1;
        optionsMenu.blocksRaycasts = true;
    }

    //Function for closing the options menu by setting alpha to 0 and allowing for clicks to not be made
    public void CloseOptions()
    {
        optionsMenu.alpha = 0;
        optionsMenu.blocksRaycasts = false;
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
