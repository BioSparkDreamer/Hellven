using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector3 respawnPoint;
    public bool isExiting;
    public string winMenuLevel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        respawnPoint = PlayerController.instance.transform.position;
    }

    void Update()
    {

    }

    public void Respawn()
    {
        PlayerController.instance.transform.position = respawnPoint;
        Debug.Log("Respawn Player");
    }

    public void ExitingLevel()
    {
        StartCoroutine(ExitingCO());
    }

    public IEnumerator ExitingCO()
    {
        isExiting = true;
        PauseMenu.instance.canPause = false;
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(winMenuLevel);

    }
}
