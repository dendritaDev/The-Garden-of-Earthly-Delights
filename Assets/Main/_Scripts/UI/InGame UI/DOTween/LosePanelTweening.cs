using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class LosePanelTweening : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverImagePanel;

    [SerializeField]
    private GameObject exitButton;

    [SerializeField]
    private TextMeshProUGUI exitButtonText;

    [SerializeField]
    private RectTransform gameOverTransform;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private AudioSource audioSource;

    private void OnEnable()
    {
        exitButton.SetActive(false);
        exitButtonText.DOFade(0f, 0f);


        audioSource.Play();

        var sequence = DOTween.Sequence()
            .Append(gameOverImagePanel.transform.DOPunchPosition(Vector3.one * 50, 1f, 100));

        sequence
            .Append(gameOverTransform.DOLocalMoveX(1000f, 1f))
            .Join(gameOverText.DOFade(0f, 1f));

        exitButton.SetActive(true);

        sequence
            .Append(exitButtonText.DOFade(1f, 1f))
            .Join(exitButton.transform.DOLocalMoveX(100f, 1f));

    }
}
