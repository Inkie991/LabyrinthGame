using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;

    private bool isPaused = false;

    private void Start()
    {
        continueButton.gameObject.SetActive(false);
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(true);
        }
    }

    public void Continue()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }
    }
}
