using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool isGamePaused = false;

    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private BoxCollider2D feetPlayerCollider;
    private void Start()
    {
        //esto es para evitar que cuando ganamos, vamos al main menu y empezamos otra partida, el tiempo esté parado, debido a que se guardo así
        //ya que cuando se gana se para el tiempo
        UnPauseGame(); 
    }
    
    //Comento el timescale, porque no puedo dejarlo a 0 como tal porque sino, no funciona el tweening de la UI.
    public void PauseGame()
    {
        isGamePaused = true;
        playerCollider.enabled = false;
        feetPlayerCollider.enabled = false;

        //Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        isGamePaused = false;
        playerCollider.enabled = true;
        feetPlayerCollider.enabled = true;
        //Time.timeScale = 1f;
    }
}
