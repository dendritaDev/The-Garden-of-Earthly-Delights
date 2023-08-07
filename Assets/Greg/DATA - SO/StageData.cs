using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss,
    SpawnObject,
    WinStage
}

[Serializable]
public class StageEvent
{
    public StageEventType eventType;
    public float time;
    public string message;

    public EnemyData enemyToSpawn;
    public GameObject objectToSpawn;
    public int count;

    public bool isRepeatedEvent; //para saber si se tiene que repetir el evento
    public float repeatEverySeconds; //tiempo en elq ue se repite
    public int repeatCount; //numero de veces que se tiene que repetir
}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
    public string stageID;
    public List<string> stageCompletionToUnlock;

}
