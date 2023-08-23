using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CloseButtonAnimation : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float rate;
    [SerializeField] private float time = 0.25f;
    [SerializeField] private Ease ease;
    private Vector3 BaseScale;

    public AudioClip hover;
    public AudioClip pressed;

    public Image circleImage;
    public Image crossImage;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        BaseScale = crossImage.transform.localScale;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        crossImage.transform.DOScale(BaseScale * rate, time)
            .SetEase(ease);

        crossImage.color = new Color(0, 0, 0, 255);

        circleImage.DOFillAmount(1, time * 5).SetEase(ease);

        audioSource.PlayOneShot(hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        crossImage.transform.DOScale(BaseScale, time)
            .SetEase(ease);

        crossImage.color = new Color(255, 255, 255, 255);

        circleImage.DOFillAmount(0, time * 5).SetEase(ease);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(pressed);
    }

}
