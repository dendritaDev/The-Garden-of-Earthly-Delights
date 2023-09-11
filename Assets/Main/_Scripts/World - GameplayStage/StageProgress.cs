using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : MonoBehaviour
{
    StageTime stageTime;
    
    //Cada 30 segundos aumentaremos la dificultad en 0.2
    [SerializeField] float progressTimeRate = 30f; 
    [SerializeField] float progressPerSplit = 0.2f;

    //Esta variable la utilizaremos para que los enemigos cada vez sean mas fuertes en función de cuanto hayamos progresado. A más duremos, más difícil.
    public float Progress
    {
        //+1 para no tener algo dividiendo a un 0 de numerador
        get
        {
            return 1f + stageTime.time / progressTimeRate * progressPerSplit;
        }
    }
    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    

    
}
