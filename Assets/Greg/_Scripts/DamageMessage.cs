using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float timeToLive = 2f;
    [SerializeField][Range(0, 1)][Tooltip("Percentaje of the timeToLive")] float timeToFadeOff = .8f;
    float disappearTimer = 2f;
    [SerializeField] float normalDmgFontSize = 6f;
    [SerializeField] float criticalDmgFontSize = 8.5f;
    [SerializeField] Color normalDmgColor = Color.white;
    [SerializeField] Color criticalDmgColor = Color.yellow;
    [SerializeField] Color fadeColor = Color.grey;
    [SerializeField][Range(-2, 0)] float minRangeXDistance = -1;
    [SerializeField][Range(0, 2)] float maxRangeXDistance = 1;
    [SerializeField][Range(0, 2)] float YDistance = 1f;

    //para compensar que la posicion siempre la coge de donde choca con el collider y a veces queda abajo, le sumamos 1 por defecto
    private float offsetYDistance = 1f;

    public bool isCriticalHit;
    
    [HideInInspector]
    public int sortingOrder;
    [HideInInspector]
    public string text;

    private TextMeshPro textMesh;
    private Vector3 moveVector;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }


    private void OnEnable()
    {
        disappearTimer = timeToLive;
        transform.position = new Vector3(transform.position.x, transform.position.y + offsetYDistance, transform.position.z);

        if (!isCriticalHit)
        {
            textMesh.fontSize = normalDmgFontSize;
            textMesh.color = normalDmgColor;
            textMesh.text = text;
        }
        else
        {
            textMesh.fontSize = criticalDmgFontSize;
            textMesh.color = criticalDmgColor;
            textMesh.text = text + "!";

        }
        textColor = textMesh.color; //necesaria para disminuir el alpha, ya que no se puede hacer directamente con el parámetro directo
        float xPos = Random.Range(minRangeXDistance, maxRangeXDistance);

        moveVector = new Vector3(xPos, YDistance * -1);
        

    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;

        float disappearSpeed = 9f;
        //Primero emepzamos a moverla y cambiarla a color oscuro
        if (disappearTimer < timeToLive * timeToFadeOff)
        {
            transform.position -= moveVector * disappearSpeed * Time.deltaTime;
            textColor = Color.Lerp(textColor, fadeColor, disappearSpeed * Time.deltaTime);
            textMesh.color = textColor;
        }

        //un poco despues empezamos a hacerla transparente
        if(disappearTimer < timeToLive * timeToFadeOff * timeToFadeOff)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;

        }

        if (disappearTimer < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
