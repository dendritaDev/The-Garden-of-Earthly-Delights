using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField] [Range(0f,1f)] float chance = 1f;

    bool isQuitting = false;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    public void CheckDrop()
    {
        if (isQuitting) //para que no se instancien y queden sueltos despues de cerrar el debug mode
            return;

        if (dropItemPrefab.Count <= 0)
        {
            Debug.LogWarning("List of drop items is empty!");
            return;
        }

        if (Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
             

            if(toDrop == null)
            {
                Debug.LogWarning("DropOndestroy, reference to dropped item is null! Check the prefab of the object which drops items on destroy!");
                return;
            }

            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }
        
    }

}
