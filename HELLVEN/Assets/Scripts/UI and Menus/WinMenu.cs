using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class WinMenu : MonoBehaviour
{
    [Header("Loading Menu Variables")]
    public float timeBetweenObjects = 1f;
    public string menuToLoad, levelName;

    [Header("Menu Object Variables")]
    public GameObject[] buttons;
    public CanvasGroup winMenu, creditsMenu;
    public TMP_Text timeText, killedText, healthLostText;
    public GameObject winObject, buttonObject, creditsButton;

    void Start()
    {
        timeText.text = "Time to Beat: " + PlayerPrefs.GetFloat(levelName + "_time").ToString("F1") + "s";
        killedText.text = "Enemies Killed: " + PlayerPrefs.GetInt(levelName + "_kills");
        healthLostText.text = "Health Lost: " + PlayerPrefs.GetInt(levelName + "_healthLost");

        StartCoroutine(ShowObjectsCo());
    }

    public void ChangeActiveButton(int buttonToChoose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChoose]);
    }

    public IEnumerator ShowObjectsCo()
    {
        yield return new WaitForSeconds(timeBetweenObjects);
        winObject.SetActive(true);

        yield return new WaitForSeconds(timeBetweenObjects);
        timeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeBetweenObjects);
        killedText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeBetweenObjects);
        healthLostText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeBetweenObjects);
        buttonObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsButton);
    }

    //Function for turning off start screen and opening the main menu on by setting alpha to 1
    public void OpenMainMenu()
    {
        winMenu.alpha = 1;
        winMenu.blocksRaycasts = true;
    }

    //Function for closing the main menu by setting alpha to 0 and allowing for clicks to not be made
    public void CloseMainMenu()
    {
        winMenu.alpha = 0;
        winMenu.blocksRaycasts = false;
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuToLoad);
    }

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
