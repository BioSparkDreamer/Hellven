using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinMenu : MonoBehaviour
{
    [Header("Loading Menu Variables")]
    public string menuToLoad;

    [Header("Menu Object Variables")]
    public GameObject[] buttons;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeActiveButton(int buttonToChoose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChoose]);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

}
