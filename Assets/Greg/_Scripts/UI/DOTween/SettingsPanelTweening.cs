using DG.Tweening;
using UnityEngine;

public class SettingsPanelTweening : MonoBehaviour
{
    public float fadeTime = 0.25f;
    public Ease fadeEase;
    public float transformTime = 0.25f;
    public Ease transformEase;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
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
            .Join(rectTransform.DOAnchorPos(new Vector2(0, 0), transformTime)).SetEase(transformEase);
        

        audioSource.PlayOneShot(fadeIn);
    }

    public void FadeOut(bool playAudio = true)
    {
        var sequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(0f, fadeTime)).SetEase(fadeEase)
            .Join(rectTransform.DOAnchorPos(new Vector2(-2000, 0), transformTime)).SetEase(transformEase);

        if (playAudio)
            audioSource.PlayOneShot(fadeOut);
    }
}
