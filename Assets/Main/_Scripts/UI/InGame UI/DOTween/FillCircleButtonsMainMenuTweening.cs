using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class FillCircleButtonsMainMenuTweening : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float rate = 1.25f;
    [SerializeField] private float time = 0.25f;
    [SerializeField] private Ease ease;
    
    private Vector3 BaseScale;

    public AudioClip hover;
    public AudioClip pressed;

    public Image circleImage;
    public RectTransform textTransform;
    public TextMeshProUGUI text;

    private AudioSource audioSource;
    private Color textBaseColor;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        BaseScale = textTransform.transform.localScale;
        textBaseColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textTransform.transform.DOScale(BaseScale * rate, time)
            .SetEase(ease);

        text.color = new Color(255, 255, 255, 255);

        circleImage.DOFillAmount(1, time * 5).SetEase(ease);

        audioSource.PlayOneShot(hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textTransform.transform.DOScale(BaseScale, time).SetEase(ease);

        text.color = textBaseColor;

        circleImage.DOFillAmount(0, time * 5).SetEase(ease);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(pressed);
    }


}
