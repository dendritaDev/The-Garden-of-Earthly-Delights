using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTime : MonoBehaviour
{
    public float time;
    TimerUI timerUI;
    private PauseManager pauseManager;

    private void Awake()
    {
        timerUI = FindObjectOfType<TimerUI>();
    }

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseManager.isGamePaused)
            return;

        time += Time.deltaTime;
        timerUI.UpdateTime(time);
    }
}
