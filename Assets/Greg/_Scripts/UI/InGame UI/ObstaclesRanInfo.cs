using UnityEngine;

public class ObstaclesRanInfo : MonoBehaviour
{
    public Transform objectToRotate;
    public SpriteRenderer splashTexture;
    public Sprite[] splashImages;

    void Start()
    {
        splashTexture.sprite = splashImages[Random.Range(0, splashImages.Length)];
        Quaternion quaternion = Quaternion.EulerRotation(0, 0, Random.Range(0, 360));
        objectToRotate.rotation = quaternion;
    }

}
