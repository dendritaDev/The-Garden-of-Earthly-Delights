using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UpgradeDescriptionPanelTweening : MonoBehaviour
{
    [SerializeField] private RectTransform FrameRectTransform;
    [SerializeField] private Ease RectTransformEase;
    [SerializeField] private float transformTime = 0.15f;
    [SerializeField] private RectTransform TitleTextTransform;
    [SerializeField] private Ease TitleTextEase;
    [SerializeField] private float titleTextTime = 0.15f;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private Ease DescritionTextEase;
    [SerializeField] private float descriptionTextTime = 1f;

    private DOTweenTMPAnimator TitleTextAnimator;

    public void TweenDescriptionPanel()
    {
        Initialize();

        TitleTextAnimator = new DOTweenTMPAnimator(DescriptionText);

        var sequence = DOTween.Sequence()
            .Append(FrameRectTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), transformTime)).SetEase(RectTransformEase)
            .Append(TitleTextTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), titleTextTime)).SetEase(TitleTextEase).OnComplete(() =>
            {
                TweenText();
            });
    }

    public void TweenText()
    {

        var sequence = DOTween.Sequence();
        float time = descriptionTextTime / (float)TitleTextAnimator.textInfo.characterCount;
        float constTime = time;
        
        for (int i = 0; i < TitleTextAnimator.textInfo.characterCount; i++)
        {
            sequence
                .Append(TitleTextAnimator.DOFadeChar(i, 1, time)).SetEase(DescritionTextEase)
                .Join(TitleTextAnimator.DOPunchCharScale(i, 1.5f, time)).SetEase(DescritionTextEase);

            time -= (constTime / (float)TitleTextAnimator.textInfo.characterCount);
        }
    }

    private void Initialize()
    {   
        DescriptionText.DOFade(0f, 0f);
    }
}
