using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMessage : PopupMessage
{
    public float disappearSpeed = 9f;
    public override void CriticalHit()
    {
        textMesh.fontSize = popupData.criticalFontSize;
        textMesh.color = popupData.criticalColor;
        textMesh.text = "+" + text + "!";
    }

    public override void NormalHit()
    {
        textMesh.fontSize = popupData.normalFontSize;
        textMesh.color = popupData.mainColor;
        textMesh.text = "+" + text;
    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;

        
        //Primero emepzamos a moverla y cambiarla a color oscuro
        if (disappearTimer < popupData.timeToLive * popupData.timeToFadeOff)
        {
            transform.position -= moveVector * disappearSpeed * Time.deltaTime;
            textColor = Color.Lerp(textColor, popupData.fadeColor, disappearSpeed * Time.deltaTime);
            textMesh.color = textColor;
        }

        //un poco despues empezamos a hacerla transparente
        if (disappearTimer < popupData.timeToLive * popupData.timeToFadeOff * popupData.timeToFadeOff)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;

        }

        if (disappearTimer < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
