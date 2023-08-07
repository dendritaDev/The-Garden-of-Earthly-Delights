using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStageButton : MonoBehaviour
{
    public StageData stageData;
    public void StartGameplay(string stageToPlay)
    {
        //Esta ser� la escena principal (que justamente ser� la que compartiran varios niveles seguramente)
        //Single desactiva todas las que hay activas en ese momento y adem�s te permite a�adirle otras una vez se ha cargado que es lo que hacemos con
        //GameplayStage
        SceneManager.LoadScene("Essential", LoadSceneMode.Single);
        
        //Additivamente se
        SceneManager.LoadScene(stageToPlay, LoadSceneMode.Additive);
    }
}
