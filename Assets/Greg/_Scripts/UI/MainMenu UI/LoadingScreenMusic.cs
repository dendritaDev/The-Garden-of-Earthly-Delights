using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingScreenMusic : MonoBehaviour
{
    [SerializeField] UnityEvent myUnityEvent;
    private void OnEnable()
    {
        myUnityEvent.Invoke();
    }
}
