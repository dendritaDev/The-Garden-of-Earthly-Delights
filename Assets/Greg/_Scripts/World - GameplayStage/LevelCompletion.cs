using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;

    StageTime stageTime;
    PauseManager pauseManager;

    [SerializeField] GameWinPanel levelCompletedPanel;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
        pauseManager = FindObjectOfType<PauseManager>();
        levelCompletedPanel = FindObjectOfType<GameWinPanel>(true); //(true) --> esto lo que hace es que puedas encontrarlo a pesar de que este desactivado
    }

    public void Update()
    {
        if(stageTime.time > timeToCompleteLevel)
        {
            pauseManager.PauseGame();
            levelCompletedPanel.gameObject.SetActive(true);
        }
    }
}
