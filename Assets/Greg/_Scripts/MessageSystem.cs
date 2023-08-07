using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    [SerializeField] GameObject damageMessage;

    List<TMPro.TextMeshPro> messagePool;
    int objectCount = 10;
    int count;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        messagePool = new List<TMPro.TextMeshPro>();
        for (int i = 0; i < objectCount; i++)
        {
            Populate();
        }
    }

    public void Populate()
    {
        GameObject objectDamageMessage = Instantiate(damageMessage, transform);
        messagePool.Add(objectDamageMessage.GetComponent<TMPro.TextMeshPro>());
        objectDamageMessage.SetActive(false);
    }
    public void PostMessage(string text, Vector3 worldPosition)
    {
        messagePool[count].gameObject.SetActive(true);
        messagePool[count].transform.position = worldPosition;
        messagePool[count].text = text;
        count += 1;
        if (count >= objectCount)
            count = 0;
        
    }
}
