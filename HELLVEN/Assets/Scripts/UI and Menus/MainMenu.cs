using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Object Variables")]
    public GameObject[] buttons;
    public CanvasGroup mainMenu, controlsMenu, optionsMenu, creditsMenu;
    public GameObject startMenu;

    //Function for switching the active button using the eventsystem using a parameter
    public void ChangeActiveButton(int buttonToChoose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChoose]);
    }

    //Function for turning off start screen and opening the main menu on by setting alpha to 1
    public void OpenMainMenu()
    {
        if (startMenu.activeInHierarchy)
            startMenu.SetActive(false);

        mainMenu.alpha = 1;
        mainMenu.blocksRaycasts = true;
    }

    //Function for closing the main menu by setting alpha to 0 and allowing for clicks to not be made
    public void CloseMainMenu()
    {
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;
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

    //Function for quitting out of the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void PlayButtonPress()
    {
        AudioManager.instance.PlaySFX(13);
    }
}
