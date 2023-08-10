using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    [SerializeField] GameObject damageMessage;
    [SerializeField] GameObject healMessage;

    List<PopupMessage> damageMessagePool;
    List<PopupMessage> healMessagePool;
    
    [SerializeField]int poolSize = 10;
    int healCount;
    int dmgCount;

    private int sortingOrder;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        damageMessagePool = new List<PopupMessage>();
        healMessagePool = new List<PopupMessage>();
        for (int i = 0; i < poolSize; i++)
        {
            Populate();
        }
    }

    public void Populate()
    {
        GameObject objectDamageMessage = Instantiate(damageMessage, transform);
        damageMessagePool.Add(objectDamageMessage.GetComponent<PopupMessage>());
        objectDamageMessage.SetActive(false);

        GameObject objectHealMessage = Instantiate(healMessage, transform);
        healMessagePool.Add(objectHealMessage.GetComponent<PopupMessage>());
        objectHealMessage.SetActive(false);
    }

    public void DamagePopup(string damageAmount, Vector3 worldPosition, bool isCriticalHit = false)
    {
        PrintPopup(damageMessagePool, ref dmgCount, damageAmount, worldPosition, isCriticalHit);
    }

    public void HealPopup(string damageAmount, Vector3 worldPosition, bool isCriticalHit = false)
    {
        PrintPopup(healMessagePool, ref healCount,damageAmount, worldPosition, isCriticalHit);
    }

    public void PrintPopup(List<PopupMessage> pool, ref int count, string damageAmount, Vector3 worldPosition, bool isCriticalHit = false)
    {
        pool[count].text = damageAmount;
        pool[count].transform.position = worldPosition;
        pool[count].isCriticalHit = isCriticalHit;
        
        sortingOrder++;
        pool[count].sortingOrder = this.sortingOrder; //para que cada nuevo se printe encima de los antiguos

        pool[count].gameObject.SetActive(true);

        count += 1;
        if (count >= poolSize)
            count = 0;

    }

}
