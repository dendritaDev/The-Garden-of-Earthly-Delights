using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchingImagesMainMenu : MonoBehaviour
{
    [SerializeField] Image backImage;
    [SerializeField] Image mainImage;
    [SerializeField] float fadeTime = 3f;
    [SerializeField] float changeSpriteTime = 10f;
    [SerializeField] Ease disappearingImageEase;
    [SerializeField] Ease appearingImageEase;

    [SerializeField] private List<Sprite> spritesList = new List<Sprite>();
    private void Awake()
    {
        backImage = GetComponent<Image>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        int selectedImage = Random.Range(0, spritesList.Count);
        mainImage.sprite = spritesList[selectedImage];

        StartCoroutine(SwitchBacgroundImagesOverTime());
    }

    private void Update()
    {
        Debug.Log(mainImage.color.a);
    }

    public Sprite GetNextSprite(Sprite currentSprite)
    {
        List<Sprite> availableSprites = new List<Sprite>(spritesList);
        availableSprites.Remove(currentSprite);

        if (availableSprites.Count > 0)
        {
            int randomIndex = Random.Range(0, availableSprites.Count);
            return availableSprites[randomIndex];
        }
        else
        {
            return null; 
        }
    }

    public IEnumerator SwitchBacgroundImagesOverTime()
    {

        while(true)
        {
            yield return new WaitForSeconds(changeSpriteTime);

            //Le damos a la imagen que esta por detras una imagen nueva
            backImage.sprite = GetNextSprite(backImage.sprite);
            backImage.DOFade(0, 0);


            //Vamos haciendo desaparecer la imagen principal y aparecer la que está detrás
            var sequence = DOTween.Sequence()
                .Join(mainImage.DOFade(0, fadeTime)).SetEase(disappearingImageEase)
                .Join(backImage.DOFade(1, fadeTime * 2f)).SetEase(appearingImageEase)
                .OnComplete(() =>
                {
                    mainImage.sprite = backImage.sprite;
                    mainImage.color = Color.white;
                });

        }

    }

}
