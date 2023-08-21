using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class ExitToMenu : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    public void BackToMenu()
    {
        Time.timeScale = 1f;

        transitionImage.transform.DOScale(35, 1f).SetEase(Ease.InBounce);

        StartCoroutine(DelayedBackToMainMenu());
        
    }

    private IEnumerator DelayedBackToMainMenu()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");

        yield return null;
    }
}
