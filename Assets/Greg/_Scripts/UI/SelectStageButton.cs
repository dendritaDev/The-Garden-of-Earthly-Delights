using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectStageButton : MonoBehaviour
{
    public StageData stageData;
    
    public GameObject loadingScreen;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    
    public Slider progressBar;
    public Sprite background;
    public Image backgroundImage;
    public TextMeshProUGUI tipsText;
    public TextMeshProUGUI levelText;
    public CanvasGroup alphaTextHintsCanvas;
    public string[] tips;
    

    private float totalSceneProgress;
    public void StartGameplay(string stageToPlay)
    {
        //Esta será la escena principal (que justamente será la que compartiran varios niveles seguramente)
        //Single desactiva todas las que hay activas en ese momento y además te permite añadirle otras una vez se ha cargado que es lo que hacemos con
        //GameplayStage
        SceneManager.LoadScene("Essential", LoadSceneMode.Single);
        
        //Additivamente se
        SceneManager.LoadScene(stageToPlay, LoadSceneMode.Additive);
        
    }

    public void LoadGame(int stageToPlay)
    {
        backgroundImage.sprite = background;
        levelText.text = $"{levelText.text} {stageToPlay-1}";
        loadingScreen.SetActive(true);

        StartCoroutine(GenerateTips());

        scenesLoading.Add(SceneManager.LoadSceneAsync("Essential", LoadSceneMode.Single));
        scenesLoading.Add(SceneManager.LoadSceneAsync(stageToPlay, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        float progress = 0;
        
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            scenesLoading[i].allowSceneActivation = false;
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100; //Porcentaje de carga
                
                progress = Mathf.MoveTowards(progress, totalSceneProgress, Time.deltaTime);
                
                progressBar.value = progress;
                
                if(progress >= 0.95f)
                {
                    progressBar.value = 1;
                    for (int j = 0; j < scenesLoading.Count; j++)
                    {
                        scenesLoading[j].allowSceneActivation = true;
                    }
                }

                yield return new WaitForSeconds(0.01f);
                //yield return null;
            }
        }

        loadingScreen.SetActive(true);
    }

    public int tipCount;
    public IEnumerator GenerateTips()
    {
        tipCount = Random.Range(0, tips.Length);
        tipsText.text = tips[tipCount];

        while(loadingScreen.activeInHierarchy)
        {
            yield return new WaitForSeconds(1f);

            alphaTextHintsCanvas.DOFade(0, 0.2f).SetEase(Ease.InExpo);

            yield return new WaitForSeconds(0.2f);

            tipCount++;
            if (tipCount >= tips.Length)
                tipCount = 0;

            tipsText.text = tips[tipCount];

            alphaTextHintsCanvas.DOFade(1, 0.2f).SetEase(Ease.InExpo);
        }

    }
}
