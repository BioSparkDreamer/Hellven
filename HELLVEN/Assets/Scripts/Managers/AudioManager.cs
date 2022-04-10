using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source Variables")]
    public AudioSource[] sfxEffects;
    public AudioSource levelMusic, menuMusic, pauseMusic, cutSceneMusic;
    public bool isLevel, isMenu, isCutScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Play Music Depending on what the scene is
    void Start()
    {
        if (isLevel)
        {
            levelMusic.Play();
        }

        if (isMenu)
        {
            menuMusic.Play();
        }

        if (isCutScene)
        {
            cutSceneMusic.Play();
        }
    }

    //Function for Playing and Stopping Pause Music
    public void PlayPauseMusic()
    {
        if (!pauseMusic.isPlaying)
        {
            levelMusic.Pause();
            pauseMusic.Play();
        }
        else
        {
            levelMusic.Play();
            pauseMusic.Pause();
        }
    }

    //Function for being sound effects
    public void PlaySFX(int sfxToPlay)
    {
        sfxEffects[sfxToPlay].Stop();
        sfxEffects[sfxToPlay].Play();
    }

    //Function for playing adjusted sound effects
    public void PlaySFXAdjusted(int sfxToPlay)
    {
        sfxEffects[sfxToPlay].pitch = Random.Range(.8f, 1.2f);
        sfxEffects[sfxToPlay].Play();
    }
}
