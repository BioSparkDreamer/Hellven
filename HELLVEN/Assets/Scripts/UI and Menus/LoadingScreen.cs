using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [Header("Loading Screen Variables")]
    public Slider loadingBar;
    public CanvasGroup loadingScreen;
    public TMP_Text loadingText;

    public void LoadScene(string levelToLoad)
    {
        loadingScreen.alpha = 1;
        loadingScreen.blocksRaycasts = true;

        StartCoroutine(LoadLevelCo(levelToLoad));
    }

    public IEnumerator LoadLevelCo(string levelToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;

            if (operation.progress >= .9f)
            {
                loadingText.text = "Press Any Key To Continue";

                if (Input.anyKeyDown && !operation.allowSceneActivation)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
