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
    [SerializeField] private Image circleFxImage;
    [SerializeField] private Image transitionImage;


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

        //transitionImage.transform.DOScale(35, 1f).SetEase(Ease.InBounce);

        StartCoroutine(DelayedStartSceneTransition(stageToPlay));

    }

    private IEnumerator DelayedStartSceneTransition(int stageToPlay)
    {
        //yield return new WaitForSeconds(1f);

        mainMenuAlpha.alpha = 0;
        loadingScreen.SetActive(true);

        //transitionImage.transform.DOScale(0, 1f).SetEase(Ease.InBounce);

        //yield return new WaitForSeconds(1f);

        StartCoroutine(GenerateTips());

        scenesLoading.Add(SceneManager.LoadSceneAsync("Essential", LoadSceneMode.Single));
        scenesLoading.Add(SceneManager.LoadSceneAsync(stageToPlay, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
        yield return null;
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
                mainCharacterImage.fillAmount = progress;
                circleFxImage.fillAmount = progress;

                if (progress >= 0.95f)
                {
                    //transitionImage.transform.DOScale(75, .15f).SetEase(Ease.InBounce);

                    Invoke(nameof(SceneActivation),1f);
                }

                yield return new WaitForSeconds(0.01f);
                //yield return null;
            }
        }

        loadingScreen.SetActive(false);
    }

    private void SceneActivation()
    {
        progressBar.value = 1;
        for (int j = 0; j < scenesLoading.Count; j++)
        {
            scenesLoading[j].allowSceneActivation = true;
        }
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
