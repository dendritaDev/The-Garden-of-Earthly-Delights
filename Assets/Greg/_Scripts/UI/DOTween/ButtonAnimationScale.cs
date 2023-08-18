using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimationScale : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float rate;
    [SerializeField] private float time = 0.25f;
    [SerializeField] private Ease ease;
    private Vector3 BaseScale;

    public AudioClip hover;
    public AudioClip pressed;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        BaseScale = transform.localScale;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(BaseScale * rate, time)
            .SetEase(ease);

        audioSource.PlayOneShot(hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(BaseScale, time)
            .SetEase(ease);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(pressed);
    }
}
