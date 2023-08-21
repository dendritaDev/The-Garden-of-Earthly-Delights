using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;


public class InitialSceneTransitionGamePlay : MonoBehaviour
{
    [SerializeField] private Image transitionImage;


    // Start is called before the first frame update
    void Start()
    {
        transitionImage.transform.DOScale(0, 1f).SetEase(Ease.InBounce);

    }

}
