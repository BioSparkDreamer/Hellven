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
    public bool isPaused;
    public GameObject resumeButton;
    public bool canPause = true;

    [Header("Loading to Main Menu")]
    public string loadToMenu;

    [Header("Pause Menu Variables")]
    public GameObject[] buttons;

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
        if (!pauseScreen.activeInHierarchy && !isPaused)
        {
            //Turning on the pause menu and pausing the game
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;

            //Setting the current selected button to the resume button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void ChangeTimeScale()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void ChangeActiveButton(int buttonToChoose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChoose]);
    }

    public void RestartLevel()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadToMenu()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(loadToMenu);
    }
}
