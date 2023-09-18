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
    private Ease textEase;

    [SerializeField]
    private Ease textEaseFadeOut;

    [SerializeField]
    public float transformTimeTime = 0.25f;

    [SerializeField]
    private float textTime = 0.25f;
    
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        rectTransform.DOAnchorPosX(-1700, 0f);

        StartCoroutine(TextTweening());
    }


    public IEnumerator TextTweening()
    {
        yield return new WaitForSeconds(1f);

        var sequence = DOTween.Sequence()
            .Append(rectTransform.DOAnchorPos(new Vector2(0, 0), transformTimeTime)).SetEase(textEase)
            .Join(text.DOFade(1, textTime).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                FadeOut();

            });


    }

    public void FadeOut()
    {

        var sequence = DOTween.Sequence()
            .Append(rectTransform.DOAnchorPos(new Vector2(1700, 0), transformTimeTime+0.5f)).SetEase(textEaseFadeOut)
            .Join(text.DOFade(0, textTime+0.5f).SetEase(Ease.Linear));
    }

}
