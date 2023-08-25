using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField][Range(0f, 1f)] float probability;
    [SerializeField]
    [Tooltip("Distance from map boundary limits")]
    float limitOffset = 1f;
    float leftLimit, rightLimit, topLimit, bottomLimit;

    public void Start()
    {
        CalculateMapBoundaries();
    
    }

    public void Spawn()
    {
        if(Random.value < probability)
        {
            SpawnManager.instance.SpawnObject(RandomPositionBetweenBoundaries(), objectToSpawn);
        }
    }

    public Vector3 RandomPositionBetweenBoundaries()
    {
        float xPos = Random.Range(leftLimit, rightLimit);
        float yPos = Random.Range(bottomLimit, topLimit);

        return new Vector3(xPos, yPos, 0);
    }



    private void CalculateMapBoundaries()
    {
        GameObject[] boundaries = GameObject.FindGameObjectsWithTag("Boundary");

        if (boundaries.Length > 0)
        {

            leftLimit = rightLimit = boundaries[0].transform.position.x;
            topLimit = bottomLimit = boundaries[0].transform.position.y;


            for (int i = 1; i < boundaries.Length; i++)
            {
                float posX = boundaries[i].transform.position.x;
                float posY = boundaries[i].transform.position.y;

                if (posX < leftLimit)
                    leftLimit = posX;
                else if (posX > rightLimit)
                    rightLimit = posX;

                if (posY > topLimit)
                    topLimit = posY;
                else if (posY < bottomLimit)
                    bottomLimit = posY;
            }
        }

        leftLimit += limitOffset;
        rightLimit -= limitOffset;
        topLimit -= limitOffset;
        bottomLimit += limitOffset;
    }
}
