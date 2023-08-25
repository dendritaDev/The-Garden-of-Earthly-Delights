using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ButtonAnimationScaleWithFX : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float rate;
    [SerializeField] private float time = 0.25f;
    [SerializeField] private Ease ease;
    private Vector3 BaseScale;

    public AudioClip hover;
    public AudioClip pressed;

    private AudioSource audioSource;

    [SerializeField] UnityEvent myUnityEvent;
    [SerializeField] GameObject ShaderGraphObject;
    RippleWatterEffectController rippleWatterEffectController;

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
        
        ShaderGraphObject.SetActive(true);  

        StartCoroutine(TriggetEvent());
    }

    

    public IEnumerator TriggetEvent()
    {
        yield return new WaitForSeconds(0.7f);

        myUnityEvent.Invoke();


    }

}
