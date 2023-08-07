using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float timeToLive = 2f;
    float timeToDisappear = 2f;


    private void OnEnable()
    {
        timeToDisappear = timeToLive;
    }

    private void Update()
    {
        timeToDisappear -= Time.deltaTime;
        if(timeToDisappear < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
