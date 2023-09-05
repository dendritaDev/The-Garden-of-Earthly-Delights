using System.Collections;
using System.Collections.Generic;
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

        GO.transform.parent = paintingCanvas.transform;

        yield return new WaitForEndOfFrame();

        Destroy(gameObject);


    }
}
