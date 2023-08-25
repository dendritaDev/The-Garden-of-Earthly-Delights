using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RippleWatterEffectController : MonoBehaviour
{
    [SerializeField] Material rippleWaterEffect;
    [SerializeField] SpriteRenderer rippleWaterImage;
    [SerializeField] Image BackgroundImage;
    [SerializeField] [Range(0,10)] float ripplesCount;
    [SerializeField] Ease ripplesCountEase;
    [SerializeField] [Range(0,5)] float rippleSpeed;
    [SerializeField] Ease rippleSpeedEase;
    [SerializeField] [Range(0,2)] float rippleStrength;
    [SerializeField] Ease rippleStrengthEase;
    [SerializeField] [Range(0,2)] float rippleTime;
    [SerializeField] Ease rippleTimeEase;
    [SerializeField] Ease scaleEase;
    [SerializeField] float timeToScale = 1.2f;
    [SerializeField] float timeToFade = 1.2f;
    [SerializeField] Ease fadeEase;


    string ripplesCountProperty = "_RipplesCount";
    string rippleSpeedProperty = "_RippleSpeed";
    string rippleStrengthProperty = "_RippleStrength";
    string rippleTimeProperty = "_FloatTime";
    string rippleVectorCenterProperty = "_RippleCenter";

    Vector3 scale;
    float baseRipplesCount;
    float baseRippleSpeed;
    float baseRippleStrength;
    float baseRippleTime;

    private void Start()
    {
        scale = transform.localScale;
        baseRipplesCount = rippleWaterEffect.GetFloat(ripplesCountProperty);
        baseRippleSpeed = rippleWaterEffect.GetFloat(rippleSpeedProperty);
        baseRippleStrength = rippleWaterEffect.GetFloat(rippleStrengthProperty);
        baseRippleTime = rippleWaterEffect.GetFloat(rippleTimeProperty);

    }

    private void GetWaterColor()
    {
        if(BackgroundImage.color.a < 0.5f)
        {
            rippleWaterImage.color = Color.white;
            return;
        }

        switch (BackgroundImage.sprite.name)
        {
            case "BlueWorld":
                rippleWaterImage.color = new Color(0.29f, 0.67f, 0.78f, 1f);
                break;

            case "Cave":
                rippleWaterImage.color = new Color(0.28f, 0.30f, 0.15f, 1f);
                break;

            case "Hole":
                rippleWaterImage.color = new Color(0.37f, 0.43f, 0.24f, 1f);
                break;

            case "Lava":
                rippleWaterImage.color = new Color(0.28f, 0.18f, 0.18f, 1f);
                break;

            case "Stairs":
                rippleWaterImage.color = new Color(0.05f, 0.19f, 0.19f, 1f);
                break;

            default:
                rippleWaterImage.color = Color.white;
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            GetWaterColor();
            transform.DOScale(new Vector3(4f, 4f, 1f), timeToScale).SetEase(scaleEase);
            rippleWaterEffect.DOFloat(ripplesCount, ripplesCountProperty, 1f).SetEase(ripplesCountEase);
            rippleWaterEffect.DOFloat(rippleSpeed, rippleSpeedProperty, 1f).SetEase(rippleSpeedEase);
            rippleWaterEffect.DOFloat(rippleStrength, rippleStrengthProperty, 1f).SetEase(rippleStrengthEase);
            rippleWaterEffect.DOFloat(rippleTime, rippleTimeProperty, 1f).SetEase(rippleTimeEase);
            rippleWaterImage.DOFade(0f, timeToFade).SetEase(fadeEase);

        }

        if(Input.GetKeyUp(KeyCode.R))
        {
            transform.DOScale(scale, 0f);
            rippleWaterEffect.DOFloat(baseRipplesCount, ripplesCountProperty, 0f);
            rippleWaterEffect.DOFloat(baseRippleSpeed, rippleSpeedProperty, 0f);
            rippleWaterEffect.DOFloat(baseRippleStrength, rippleStrengthProperty, 0f);
            rippleWaterEffect.DOFloat(baseRippleTime, rippleTimeProperty, 0f);
            rippleWaterImage.DOFade(1f, 0f);
            
        }
    }

    public void SetVectorCenter(Vector2 VectorCenter)
    {
        rippleWaterEffect.SetVector(rippleVectorCenterProperty, VectorCenter);
    }

    private void OnEnable()
    {
        GetWaterColor();
        rippleWaterEffect.DOFloat(ripplesCount, ripplesCountProperty, 1f).SetEase(ripplesCountEase);
        rippleWaterEffect.DOFloat(rippleSpeed, rippleSpeedProperty, 1f).SetEase(rippleSpeedEase);
        rippleWaterEffect.DOFloat(rippleStrength, rippleStrengthProperty, 1f).SetEase(rippleStrengthEase);
        rippleWaterEffect.DOFloat(rippleTime, rippleTimeProperty, 1f).SetEase(rippleTimeEase);
        rippleWaterImage.DOFade(0f, timeToFade).SetEase(fadeEase);
        transform.DOScale(new Vector3(3f, 3f, 1f), timeToScale).SetEase(scaleEase).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        transform.DOScale(scale, 0f);
        rippleWaterEffect.DOFloat(baseRipplesCount, ripplesCountProperty, 0f);
        rippleWaterEffect.DOFloat(baseRippleSpeed, rippleSpeedProperty, 0f);
        rippleWaterEffect.DOFloat(baseRippleStrength, rippleStrengthProperty, 0f);
        rippleWaterEffect.DOFloat(baseRippleTime, rippleTimeProperty, 0f);
        rippleWaterImage.DOFade(1f, 0f);
    }
}
