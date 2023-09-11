using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTime(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        text.text = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));/* minutes.ToString() + ":" + seconds.ToString();*/
    }
}
