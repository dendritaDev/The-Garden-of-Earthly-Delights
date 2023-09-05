using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetState(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}
