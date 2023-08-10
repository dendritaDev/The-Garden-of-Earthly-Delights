using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    [SerializeField] GameObject damageMessage;

    List<DamageMessage> messagePool;
    
    [SerializeField]int poolSize = 10;
    int count;

    private int sortingOrder;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        messagePool = new List<DamageMessage>();
        for (int i = 0; i < poolSize; i++)
        {
            Populate();
        }
    }

    public void Populate()
    {
        GameObject objectDamageMessage = Instantiate(damageMessage, transform);
        messagePool.Add(objectDamageMessage.GetComponent<DamageMessage>());
        objectDamageMessage.SetActive(false);
    }
    public void PostMessage(string text, Vector3 worldPosition, bool isCriticalHit = false)
    {
        messagePool[count].text = text;
        messagePool[count].transform.position = worldPosition;
        messagePool[count].isCriticalHit = isCriticalHit;
        sortingOrder++;
        messagePool[count].sortingOrder = this.sortingOrder; //para que cada nuevo se printe encima de los antiguos

        messagePool[count].gameObject.SetActive(true);

        count += 1;
        if (count >= poolSize)
            count = 0;
        
    }
}
