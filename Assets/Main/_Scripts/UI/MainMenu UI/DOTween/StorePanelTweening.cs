using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class StorePanelTweening : MonoBehaviour
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

    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //FadeOut(false);
    }

    public void FadeIn()
    {
        
        var sequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(1f, fadeTime)).SetEase(fadeEase)
            .Join(rectTransform.DOAnchorPos(new Vector2(0,0), transformTime)).SetEase(transformEase);

        StartCoroutine(PlaySFX());
        StartCoroutine(nameof(ItemsAnimation));
    }

    public IEnumerator PlaySFX()
    {
        yield return new WaitForSeconds(0.12f);
        audioSource.PlayOneShot(fadeIn);
    }

    IEnumerator ItemsAnimation()
    {
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

    public void FadeOut(bool playAudio = true)
    {
        var sequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(0f, fadeTime)).SetEase(fadeEase)
            .Join(rectTransform.DOAnchorPos(new Vector2(2000, 0), transformTime)).SetEase(transformEase);

        if(playAudio)
            audioSource.PlayOneShot(fadeOut);
    }
}
