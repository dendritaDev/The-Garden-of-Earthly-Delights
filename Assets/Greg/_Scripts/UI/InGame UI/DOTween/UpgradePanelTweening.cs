using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class UpgradePanelTweening : MonoBehaviour
{
    [SerializeField] private CanvasGroup DialogCanvasGroup;
    [SerializeField] private RectTransform FrameRectTransform;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private CanvasGroup ButtonCloseCanvasGroup;
    [SerializeField] private List<Image> UpgradesImageList;
    [SerializeField] private List<Image> BackgroundUpgradesImageList;

    private DOTweenTMPAnimator TitleTextAnimator;
    private Vector2 FrameSizeDelta;

    private void OnEnable()
    {
        Initialize();

        TitleTextAnimator = new DOTweenTMPAnimator(TitleText);

        FrameRectTransform.sizeDelta = new Vector2(-1, 171);


        var sequence = DOTween.Sequence()
            .Append(DialogCanvasGroup.DOFade(1f, 0.2f))
            .Join(FrameRectTransform.DOSizeDelta(new Vector2(FrameSizeDelta.x, 171), 1f))
            .Append(FrameRectTransform.DOSizeDelta(new Vector2(FrameSizeDelta.x, FrameSizeDelta.y), 1f));

        for (int i = 0; i < TitleTextAnimator.textInfo.characterCount; i++)
        {
            sequence
                .Append(TitleTextAnimator.DOFadeChar(i, 1, 0.1f))
                .Join(TitleTextAnimator.DOPunchCharScale(i, 1.5f, 0.1f));
        }

        for (int i = 0; i < BackgroundUpgradesImageList.Count-1; i++)
        {
            sequence.Append(BackgroundUpgradesImageList[i].DOFade(1f, 0.25f))
            .Join(BackgroundUpgradesImageList[i].transform.DOScale(0.4112785f, 0.25f).SetEase(Ease.OutBounce))
            .Join(UpgradesImageList[i].DOFade(1f, 0.25f));
        }

        sequence.Append(ButtonCloseCanvasGroup.DOFade(1.0f, 0.25f))
            .OnComplete(() => DialogCanvasGroup.blocksRaycasts = true)
            .Play();

    }

    public void Initialize()
    {
        foreach (var image in UpgradesImageList)
        {
            image.DOFade(0f, 0f).Play();
            image.transform.localScale = Vector3.zero;
        }

        foreach (var image in BackgroundUpgradesImageList)
        {
            image.DOFade(0f, 0f).Play();
            image.transform.localScale = Vector3.zero;
        }

        DialogCanvasGroup.alpha = 0f;
        ButtonCloseCanvasGroup.alpha = 0f;

        FrameSizeDelta = FrameRectTransform.sizeDelta;
        FrameRectTransform.sizeDelta = Vector2.zero;
        

        TitleText.alpha = 0f;

    }
}
