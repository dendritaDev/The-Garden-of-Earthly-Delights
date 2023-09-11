using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PopupData : ScriptableObject
{
    public float timeToLive = 2f;
    [Range(0, 1)][Tooltip("Percentaje of the timeToLive")] public float timeToFadeOff = .8f;
    public float normalFontSize = 6f;
    public float criticalFontSize = 8.5f;
    public Color mainColor = Color.white;
    public Color criticalColor = Color.yellow;
    public Color fadeColor = Color.grey;

    [Range(-2, 0)] public float minRangeXDistance = -1;
    [Range(0, 2)] public float maxRangeXDistance = 1;
    [Range(0, 2)] public float YDistance = 1f;

    //para compensar que la posicion siempre la coge de donde choca con el collider y a veces queda abajo, le sumamos 1 por defecto
    [HideInInspector] public float offsetYDistance = 1f;

}
