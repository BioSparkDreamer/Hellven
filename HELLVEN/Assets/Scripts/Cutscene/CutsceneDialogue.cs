using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneDialogue : MonoBehaviour
{
    [Header("Dialogue Text Variables")]
    public TMP_Text dialogueText;
    public int currentLine;
    public DialogueManager theDialogue;
    public bool isTyping;
    public float timeBetweenLetters;

    [Header("Dialogue Box Variables")]
    public GameObject dialogueBox;
    public GameObject[] dialogueBoxes;
    public int currentDialogueBox;
    public bool finishedDialogue;
    public string levelToLoad;

    void Start()
    {
        currentDialogueBox = 0;
        currentLine = 0;
    }

    void Update()
    {
        if (!finishedDialogue)
        {
            if (currentDialogueBox < 0)
                return;

            if (currentDialogueBox > dialogueBoxes.Length - 1)
                currentDialogueBox = dialogueBoxes.Length - 1;

            if (!dialogueBox.activeInHierarchy && Time.time > 0.05f)
            {
                dialogueBox.SetActive(true);
                StartDialogue();
            }

            if (dialogueBox.activeInHierarchy)
            {
                if (Input.GetButtonDown("Continue") && !isTyping)
                {
                    if (currentDialogueBox >= 0 && currentDialogueBox < dialogueBoxes.Length - 1)
                    {
                        ContinueDialogue();
                        Debug.Log("Dialogue Working");

                        dialogueBoxes[currentDialogueBox].SetActive(false);
                        AudioManager.instance.PlaySFX(13);
                        currentDialogueBox++;
                        dialogueBoxes[currentDialogueBox].SetActive(true);
                    }
                    else
                    {
                        EndDialogue();
                        Debug.Log("Ending Dialogue Because it Ended");
                    }
                }
            }

            if (Input.GetButtonDown("Skip") && !finishedDialogue)
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        if (currentLine == 0)
        {
            StartCoroutine(ShowDialogueCo(theDialogue));
        }
    }

    public IEnumerator ShowDialogueCo(DialogueManager dialog)
    {
        this.theDialogue = dialog;
        yield return new WaitForEndOfFrame();
        StartCoroutine(TypeDialogueCo(dialog.Lines[0]));
    }

    public void ContinueDialogue()
    {
        ++currentLine;

        if (currentLine < theDialogue.Lines.Count)
        {
            StartCoroutine(TypeDialogueCo(theDialogue.Lines[currentLine]));
        }
    }

    public IEnumerator TypeDialogueCo(string lineToType)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (var letter in lineToType.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        finishedDialogue = true;
        dialogueBox.SetActive(false);
        Debug.Log("Ending Dialogue");
        AudioManager.instance.PlaySFX(13);
        StartCoroutine(LoadNextLevelCo());
    }

    public IEnumerator LoadNextLevelCo()
    {
        FindObjectOfType<FadeScreen>().FadeToBlack();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(levelToLoad);
    }
}
