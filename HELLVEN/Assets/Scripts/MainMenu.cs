using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Starting Game Variables")]
    public string sceneToLoad;

    [Header("Menu Object Variables")]
    public GameObject[] buttons;
    public CanvasGroup mainMenu;
    public GameObject startMenu;

    //Function to change the active button selected using the eventsystem
    public void ChangeActiveButton(int buttonToChoose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChoose]);
    }

    //Function to open the main menu
    public void OpenMainMenu()
    {
        startMenu.SetActive(false);
        mainMenu.alpha = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
