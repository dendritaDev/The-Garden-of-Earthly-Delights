using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinPanelTweening : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer paper;

    [SerializeField]
    private GameObject paperGO;

    [SerializeField]
    private Ease paperEase;

    [SerializeField]
    private SpriteRenderer shadePaper;

    [SerializeField]
    private Transform shadePaperTransform;

    [SerializeField]
    private Ease shadePaperEase;

    [SerializeField]
    private Transform leftTape;

    [SerializeField]
    private Ease leftTapeEase;

    [SerializeField]
    private Transform rightTape;

    [SerializeField]
    private Ease rightTapeEase;

    [SerializeField]
    private Transform button;

    [SerializeField]
    private Sprite paperSpriteToChange;


    private void OnEnable()
    {
        Initialize();

        //ShaderPaper
        Color shadePaperColorEnd = new Color(0.1803922f, 0.1803922f, 0.1803922f, 1f);
        var sequence = DOTween.Sequence()
            .Append(shadePaperTransform.DOScale(10.785f, 1.5f)).SetEase(shadePaperEase)
            .Join(shadePaper.DOColor(shadePaperColorEnd, 1.5f)).SetEase(shadePaperEase);

        //end shaderpaper and start paper
        Color shadePaperColorEnd2 = new Color(0.0803922f, 0.0803922f, 0.0803922f, 1f);
        sequence
           .Append(shadePaperTransform.DOScale(5.785f, 2f)).SetEase(shadePaperEase)
           .Join(shadePaper.DOColor(shadePaperColorEnd2, 2f)).SetEase(shadePaperEase)

           .Join(paperGO.transform.DOScale(24.785f, 1.5f)).SetEase(paperEase)
           .Join(paper.DOFade(1.5f, 0.08f));

        //LeftTape
        sequence
            //.Append(leftTape.DOLocalMoveY(291.6f, 1f)).SetEase(leftTapeEase);
            .Append(leftTape.DOLocalMoveY(291.6f, 1f)).SetEase(leftTapeEase)
            .Join(leftTape.DOLocalMoveX(-357.8f, 1f)).SetEase(leftTapeEase)
            .Join(leftTape.DOScale(94.559f, 1f)).SetEase(leftTapeEase);

        //RightTape and Button activation
        sequence
            .Append(rightTape.DOLocalMoveY(-337f, 0.5f)).SetEase(rightTapeEase)
            .Join(rightTape.DOLocalMoveX(402f, 0.5f)).SetEase(rightTapeEase)
            .Join(rightTape.DOScale(67.61806f, 0.5f)).SetEase(rightTapeEase)
            .OnComplete(() =>
            {
                paperGO.GetComponent<SpriteRenderer>().sprite = paperSpriteToChange;
                button.gameObject.SetActive(true);
            });


    }

    private void Initialize()
    {
        //ShaderPaper
        shadePaperTransform.DOScale(65f, 0f);
        Color shadePaperColorStart = new Color(0.5686275f, 0.5686275f, 0.5686275f, 0);
        shadePaper.DOColor(shadePaperColorStart, 0f);

        //Paper
        paperGO.transform.DOScale(65f, 0f);
        paper.DOFade(0f, 0f);

        //LeftTape
        //leftTape.DOLocalMoveY(791.6f, 0f);
        leftTape.DOLocalMoveY(466f, 0f);
        leftTape.DOLocalMoveX(-770f, 0f);
        leftTape.DOScale(328f, 0f);

        //RightTape
        rightTape.DOLocalMoveY(-480f, 0f);
        rightTape.DOLocalMoveX(976f, 0f);
        rightTape.DOScale(278f, 0f);

        //Button
        button.gameObject.SetActive(false);

    }
}
