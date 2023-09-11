using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PoolObjectData : ScriptableObject
{
    public GameObject originalPrefab;

    //si el objeto que queremos usar en la piscina esta hecho de dos objetos por ejemplo, en el container pondriamos el segundo
    public GameObject containerPrefab;

    public int poolID;
}
