using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [Header("Variables to Use")]
    public Animator fadeScreen;

    void Start()
    {
        FadeFromBlack();
    }

    public void FadeFromBlack()
    {
        fadeScreen.SetBool("FadeOut", true);
    }

    public void FadeToBlack()
    {
        fadeScreen.SetBool("FadeIn", true);
    }
}
