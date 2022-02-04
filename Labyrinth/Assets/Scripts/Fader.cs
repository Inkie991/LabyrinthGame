using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private const string FADER_PATH = "Canvas";

    [SerializeField] private Animator animator;

    GameObject faderImage;

    private static Fader _instance;
    public static Fader instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public bool isFading { get; private set; }

    private Action _fadedInCallBack;
    private Action _fadedOutCallBack;

    public void FadeIn(Action fadedInCallBack)
    {
        if (isFading) return;

        isFading = true;
        _fadedInCallBack = fadedInCallBack;
        animator.SetBool("Faded",true);
    }

    public void FadeOut(Action fadedOutCallBack)
    {
        if (isFading) return;

        isFading = true;
        _fadedOutCallBack = fadedOutCallBack;
        animator.SetBool("Faded", false);
    }

    private void Handle_FadeInAnimationOver()
    {
        _fadedInCallBack?.Invoke();
        _fadedInCallBack = null;
        isFading = false;
    }

    private void Handle_FadeOutAnimationOver()
    {
        _fadedOutCallBack?.Invoke();
        _fadedOutCallBack = null;
        isFading = false;
    }

}
