using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnObjectsArea : SpawnObject
{
    [SerializeField] private float spawningTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        StartCoroutine(SpawnObjectsRandomly());
        
    }

    IEnumerator SpawnObjectsRandomly()
    {
        while(true)
        {
            base.Spawn();
            yield return new WaitForSeconds(spawningTime);
        }
       
        
    }

    public void StopSpawning()
    {
        StopCoroutine(SpawnObjectsRandomly());
    }
}
