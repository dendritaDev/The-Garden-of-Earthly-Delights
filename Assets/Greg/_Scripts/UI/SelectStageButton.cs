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

    [SerializeField] private CanvasGroup mainMenuAlpha;
    [SerializeField] private GameObject loadingScreen;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    
    [SerializeField] private Slider progressBar;
    [SerializeField] private Sprite background;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image mainCharacterImage;
    [SerializeField] private Image CircleFxImage;
    [SerializeField] private TextMeshProUGUI tipsText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private CanvasGroup alphaTextHintsCanvas;
    [SerializeField] private string[] tips;
    

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
        mainMenuAlpha.alpha = 0;
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

                mainCharacterImage.fillAmount = progress;
                CircleFxImage.fillAmount = progress;

                yield return new WaitForSeconds(0.01f);
                //yield return null;
            }
        }

        loadingScreen.SetActive(true);
    }

    private int tipCount;
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
