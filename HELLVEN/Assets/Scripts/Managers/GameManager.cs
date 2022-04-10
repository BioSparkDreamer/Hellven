using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("CheckPoint Variables")]
    public Vector3 respawnPoint;
    private CheckPoint[] checkPoints;

    [Header("Respawning Variables")]
    public float waitToRespawn;

    [Header("Exiting Level")]
    public string winMenuLevel;

    [Header("Score Variables")]
    public float timeInLevel;
    public int enemiesKilled;
    public int healthLost;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //Make the respawn point in level equal to the player's transform position
        respawnPoint = PlayerController.instance.transform.position;

        //Find all checkpoints in level to fill the array
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    void Update()
    {
        //Increment the time in level counter
        timeInLevel += Time.deltaTime;
    }


    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        respawnPoint = spawnPoint;
    }

    public void DeactivateCheckPoints()
    {
        for (int i = 0; i < checkPoints.Length; i++)
            checkPoints[i].ResetCheckPoint();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = respawnPoint;
        CameraController.instance.stopFollow = false;
    }

    public void ExitingLevel()
    {
        StartCoroutine(ExitingCO());
    }

    public IEnumerator ExitingCO()
    {
        PauseMenu.instance.canPause = false;
        PlayerController.instance.canMove = false;

        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_kills", enemiesKilled);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_healthLost", healthLost);

        yield return new WaitForSeconds(1f - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1.5f / UIController.instance.fadeSpeed) + 1f);

        SceneManager.LoadScene(winMenuLevel);

    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }

    public IEnumerator GameOverCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        PauseMenu.instance.canPause = false;
        AudioManager.instance.PlayPauseMusic();

        yield return new WaitForSeconds(2f);

        GameOverMenu.instance.ShowGameOverScreen();
    }
}
