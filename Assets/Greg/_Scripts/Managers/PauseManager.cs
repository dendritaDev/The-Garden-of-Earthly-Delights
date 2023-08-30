using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool isGamePaused = false;

    private void Start()
    {
        //esto es para evitar que cuando ganamos, vamos al main menu y empezamos otra partida, el tiempo esté parado, debido a que se guardo así
        //ya que cuando se gana se para el tiempo
        UnPauseGame(); 
    }
    public void PauseGame()
    {
        isGamePaused = true;
        //Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        isGamePaused = false;
        //Time.timeScale = 1f;
    }
}
