using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneReload : MonoBehaviour
{
    private const string SCENE = "SampleScene";

    private bool _isLoading;

    private static SceneReload _instance;

    private PlayerControl player;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        }

        if (player.isFinished == true)
        {
            Debug.Log("finish");
            LoadScene(SCENE);
        }

    }

    private void LoadScene(string sceneName)
    {
        if (_isLoading) return;

        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        _isLoading = true;

        var waitFading = true;
        Fader.instance.FadeIn(() => waitFading = false);
        
        while (waitFading)
        {
            yield return null;
        }
        var async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;

        waitFading = true;
        Fader.instance.FadeOut(() => waitFading = false);

        while (waitFading)
        {
            yield return null;
        }

        _isLoading = false;
    }
}
