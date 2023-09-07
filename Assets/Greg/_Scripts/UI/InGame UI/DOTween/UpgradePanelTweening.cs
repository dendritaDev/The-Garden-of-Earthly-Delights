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
            //rotate splash background
            int rot = UnityEngine.Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0, 0, rot);

            //le quitamos a la imagen la rotacion dada al bacground y le damos una rotacion solo entre -30 30 grados.
            Quaternion inverseRandomRotation = Quaternion.Inverse(randomRotation);
            BackgroundUpgradesImageList[i].transform.rotation = inverseRandomRotation;
            int rotImage = UnityEngine.Random.Range(-40, 40);
            Quaternion imageRotation = Quaternion.Euler(0, 0, rotImage);

            UpgradesImageList[i].transform.DORotateQuaternion(randomRotation, 0f);
            BackgroundUpgradesImageList[i].transform.DORotateQuaternion(imageRotation, 0f);
            UpgradesImageList[i].DOColor(UnityEngine.Random.ColorHSV(), 0f);
            UpgradesImageList[i].transform.DOLocalMoveY(UnityEngine.Random.Range(-80,80), 0f);

            sequence.Append(BackgroundUpgradesImageList[i].DOFade(1f, 0.25f))
            .Join(BackgroundUpgradesImageList[i].transform.DOScale(1f, 0.30f).SetEase(Ease.OutBounce))
            .Join(UpgradesImageList[i].DOFade(1f, 0.25f))
            .Join(UpgradesImageList[i].transform.DOScale(1f, 0.20f).SetEase(Ease.OutBounce));
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
