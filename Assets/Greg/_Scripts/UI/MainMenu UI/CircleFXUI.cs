using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CircleFXUI : MonoBehaviour
{
    [SerializeField] RectTransform FXHolder;
    [SerializeField] Image CircleImg;
    [SerializeField] TextMeshProUGUI txtProgress;
    [Range(0, 1)] float progress = 0f;

    void Update()
    {
        progress = CircleImg.fillAmount;
        txtProgress.text = Mathf.Floor(progress * 100).ToString();
        FXHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -progress * 360));
    }
}
