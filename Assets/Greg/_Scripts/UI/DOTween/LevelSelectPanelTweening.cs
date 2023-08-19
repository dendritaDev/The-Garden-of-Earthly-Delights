using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelSelectPanelTweening : MonoBehaviour
{
    public float fadeTime = 0.25f;
    public Ease fadeEase;
    public float transformTime = 0.25f;
    public Ease transformEase;
    public float nextItemTime = 0.25f;
    public Ease itemsEase;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public List<GameObject> items = new List<GameObject>();
    public AudioClip popupSFX;
    public AudioClip fadeIn;
    public AudioClip fadeOut;

    private AudioSource audioSource;
    public LevelSelectPanel levelSelectPanel;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        levelSelectPanel = GetComponent<LevelSelectPanel>();
    }


    private void Start()
    {
        FadeOut(false);
    }

    public void FadeIn()
    {

        var sequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(1f, fadeTime)).SetEase(fadeEase)
            .Join(rectTransform.DOAnchorPos(new Vector2(0, 0), transformTime)).SetEase(transformEase);

        audioSource.PlayOneShot(fadeIn);
        StartCoroutine(nameof(ItemsAnimation));
    }

    public void FadeOut(bool playAudio = true)
    {
        var sequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(0f, fadeTime)).SetEase(fadeEase)
            .Join(rectTransform.DOAnchorPos(new Vector2(0, 2000), transformTime)).SetEase(transformEase);

        if (playAudio)
            audioSource.PlayOneShot(fadeOut);
    }

    IEnumerator ItemsAnimation()
    {
        foreach (var item in levelSelectPanel.stageSelectButton)
        {
            if (item.isActiveAndEnabled)
                items.Add(item.gameObject);
        }

        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }

        yield return new WaitForSeconds(fadeTime);

        foreach (var item in items)
        {
            audioSource.PlayOneShot(popupSFX);
            item.transform.DOScale(1f, fadeTime).SetEase(itemsEase);
            yield return new WaitForSeconds(nextItemTime);
        }
    }
}
