using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            GetComponent<IPickUpObject>().OnPickUp(character);

            StartCoroutine(DestroyObject());

        }
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();

        ObstaclesRanInfo obstaclesRanInfo = gameObject.GetComponentInChildren<ObstaclesRanInfo>();

        yield return new WaitForEndOfFrame();

        GameObject GO = obstaclesRanInfo.gameObject;

        yield return new WaitForEndOfFrame();

        GameObject paintingCanvas = GameObject.FindGameObjectWithTag("Painting");

        yield return new WaitForEndOfFrame();

        SpriteRenderer spriteRenderer = GO.GetComponent<SpriteRenderer>();

        Color color = Random.ColorHSV();
        color.a = spriteRenderer.color.a;

        var sequence = DOTween.Sequence()
            .Append(spriteRenderer.DOColor(color, 0.5f))
            .Join(spriteRenderer.DOFade(Random.Range(0.15f, 0.9f), 0.5f))
            .Join(spriteRenderer.transform.DOPunchScale(new Vector3(0.2f,0.2f,0.2f), 0.5f))
            .OnComplete(()=>
            {
                spriteRenderer.sortingOrder -= 1;
            });
        
        yield return new WaitForEndOfFrame();

        GO.transform.parent = paintingCanvas.transform;


        Destroy(gameObject);


    }
}
