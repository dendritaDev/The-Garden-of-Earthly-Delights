using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchingImagesMainMenu : MonoBehaviour
{
    [SerializeField] Image backImage;
    [SerializeField] Image mainImage;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float fadeTime = 3f;
    [SerializeField] float changeSpriteTime = 10f;
    [SerializeField] Ease disappearingImageEase;
    [SerializeField] Ease appearingImageEase;

    private List<Sprite> spritesList;
    private void Awake()
    {
        backImage = GetComponent<Image>();
        spritesList = new List<Sprite>();

        for (int i = 0; i < sprites.Length; i++)
        {
            spritesList.Add(sprites[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        int selectedImage = Random.Range(0, sprites.Length);
        mainImage.sprite = sprites[selectedImage];

        StartCoroutine(SwitchBacgroundImagesOverTime());
    }

    private void Update()
    {
        Debug.Log(mainImage.color.a);
    }

    public Sprite GetNextSprite()
    {
        int randomIndex = Random.Range(0, spritesList.Count);
        return spritesList[randomIndex];
    }

    public IEnumerator SwitchBacgroundImagesOverTime()
    {

        while(true)
        {
            yield return new WaitForSeconds(changeSpriteTime);

            //Le damos a la imagen que esta por detras una imagen nueva
            backImage.sprite = GetNextSprite();
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
                //.OnComplete(OnSequenceComplete);


            //mainImage.sprite = backImage.sprite;
            //mainImage.DOFade(1, 0);

            //Invoke(nameof(OnSequenceComplete), 1.5f);
        }

    }

    private void OnSequenceComplete()
    {
        mainImage.sprite = backImage.sprite;
        mainImage.color = Color.white;
    }
}
