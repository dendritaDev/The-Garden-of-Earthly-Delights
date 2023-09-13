using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupMessage : MonoBehaviour
{
    public PopupData popupData;

    //Elementos que cambian en el tiempo y que por tanto no se pueden utilizar en un SO porque es una estructura que guarda los datos:
    //Si metia estas variables en el SO, todos funcionaban como uno, todos se movian igual, al mismo tiempo, tenian el mismo texto, color, eran criticos, etc.
    [HideInInspector] public float disappearTimer = 2f;
    [HideInInspector] public bool isCriticalHit;
    [HideInInspector] public int sortingOrder;
    [HideInInspector] public string text;
    [HideInInspector] public TextMeshPro textMesh;
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        disappearTimer = popupData.timeToLive;
        transform.position = new Vector3(transform.position.x, transform.position.y + popupData.offsetYDistance, transform.position.z);

 
        if (!isCriticalHit)
        {
            NormalHit();
        }
        else
        {
            CriticalHit();
        }

        textColor = textMesh.color; //necesaria para disminuir el alpha, ya que no se puede hacer directamente con el parámetro directo
        float xPos = Random.Range(popupData.minRangeXDistance, popupData.maxRangeXDistance);

        moveVector = new Vector3(xPos, popupData.YDistance * -1);
    }


    public virtual void NormalHit()
    {

    }

    public virtual void CriticalHit()
    {

    }
}
