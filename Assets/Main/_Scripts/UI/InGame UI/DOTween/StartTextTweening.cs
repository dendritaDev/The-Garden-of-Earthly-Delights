using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StartTextTweening : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI secondText;

    [SerializeField]
    private Ease textEase;

    [SerializeField]
    private Ease textEaseFadeOut;

    [SerializeField]
    public float transformTimeTime = 0.25f;

    [SerializeField]
    private float textTime = 0.25f;

    StaveEventManager staveEventManager;

    void Start()
    {
        staveEventManager = FindObjectOfType<StaveEventManager>();
        Initialize();
    }

    public void Initialize()
    {
        rectTransform.DOAnchorPos(new Vector2(-1700, 50), 0);
        text.text = staveEventManager.stageData.initialText;
        StartCoroutine(TextTweening());
    }


    public IEnumerator TextTweening()
    {
        yield return new WaitForSeconds(1f);

        var sequence = DOTween.Sequence()
            .Append(rectTransform.DOAnchorPos(new Vector2(0, 50), transformTimeTime, true)).SetEase(textEase)
            .Join(text.DOFade(1, textTime).SetEase(Ease.Linear))
            .Join(secondText.DOFade(1, textTime).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                FadeOut();

            });


    }

    public void FadeOut()
    {

        var sequence = DOTween.Sequence()
            .Append(rectTransform.DOAnchorPos(new Vector2(1700, 50), transformTimeTime+0.5f)).SetEase(textEaseFadeOut)
            .Join(text.DOFade(0, textTime+0.5f).SetEase(Ease.Linear))
            .Join(secondText.DOFade(0, textTime+0.5f).SetEase(Ease.Linear));

    }

}
