using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawnGroup
{
    public EnemyData enemyData;
    public int count;
    public bool isBoss;

    public float repeatTimer;
    public float timeBetweenSpawns;
    public int repeatCount;

    public EnemiesSpawnGroup(EnemyData enemyData, int count, bool isBoss)
    {
        this.enemyData = enemyData;
        this.count = count;
        this.isBoss = isBoss;
    }

    public void SetRepeatSpawn(float timeBetweenSpawns, int repeatcount)
    {
        this.timeBetweenSpawns = timeBetweenSpawns;
        this.repeatCount = repeatcount;
        repeatTimer = timeBetweenSpawns;
    }
}
public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] PoolManager poolManager;
    [SerializeField] Vector2 spawnArea;
    GameObject player;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBossHealth;
    [SerializeField] Slider bossHealthBar;

    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeatedSpawnGroupList;

    int spawnPerFrame = 2;

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>(); //pasamos true para que lo busque en objectos inactivos tmb
        stageProgress = FindObjectOfType<StageProgress>();
    }

    private void Update()
    {
        ProcessSpawn();
        ProcessRepeatedSpawnGroups();
        UpdateBossHealth();

    }

    /// <summary>
    /// Esto sirve para que las oleadas se preitan varias veces en caso de que esten asi en los parametros, simplemente añaden a la lista de enemigos mas veces
    /// </summary>
    private void ProcessRepeatedSpawnGroups()
    {
        if(repeatedSpawnGroupList == null) { return; }

        for(int i = repeatedSpawnGroupList.Count - 1; i >= 0; i--)
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if(repeatedSpawnGroupList[i].repeatTimer < 0)
            {
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawns;
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);
                repeatedSpawnGroupList[i].repeatCount -= 1;

                if(repeatedSpawnGroupList[i].repeatCount <= 0)
                {
                    repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// En vez de instanciar todos los enemigos en un mismo frame como tenia antes, ahora lo que hago con esto es que se instancie uno por cada frame, que tampoco quedará muy diferente visualmente
    /// y sin embargo, hará que no se freezee tanto el pc
    /// </summary>
    private void ProcessSpawn()
    {
        if(enemiesSpawnGroupList == null) { return; }

        for (int i = 0; i < spawnPerFrame; i++) //si deseamos que en vez de 1 por frame se puedan spawnaear mas, rollo 2 o 3 , no toda una wave, ya que sino esta ptimizacion no tendria sentido
        {
            if (enemiesSpawnGroupList.Count > 0)
            {
                if (enemiesSpawnGroupList[0].count <= 0) { return; }

                SpawnEnemy(enemiesSpawnGroupList[0].enemyData, enemiesSpawnGroupList[0].isBoss);
                enemiesSpawnGroupList[0].count -= 1;

                if (enemiesSpawnGroupList[0].count <= 0)
                {
                    enemiesSpawnGroupList.RemoveAt(0);
                }
            }
        }
    
    }

    internal void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);

        if(repeatedSpawnGroupList == null) { repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();}
        repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }

    private void UpdateBossHealth()
    {
        if(bossEnemiesList == null) { return; }
        if(bossEnemiesList.Count == 0) { return; }

        currentBossHealth = 0;

        for(int i = 0; i < bossEnemiesList.Count; i++)
        {
            if(bossEnemiesList[i] == null) { continue; }
            currentBossHealth += bossEnemiesList[i].stats.hp; //Por si un boss se cosideran varios enemigos hacemos que la barra de hp sea la suma total de su vida, para saber como vamos avanzando o cuantos nos quedan aprox
        }

        bossHealthBar.value = currentBossHealth;

        if(currentBossHealth <= 0)
        {
            bossHealthBar.gameObject.SetActive(false);
            bossEnemiesList.Clear();
        }
    }

    public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)
    {
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);

        if(enemiesSpawnGroupList == null) { enemiesSpawnGroupList = new List<EnemiesSpawnGroup>(); }

        enemiesSpawnGroupList.Add(newGroupToSpawn);
    }

    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        Vector3 spawnPosition = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea);

        spawnPosition += player.transform.position; //para spawne por donde esta el player

        //spawning main enemy object
        GameObject newEnemy = poolManager.GetObject(enemyToSpawn.poolObjectData);
        newEnemy.transform.position = spawnPosition;

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);
        
        if(isBoss)
        {
            SpawnBossEnemy(newEnemyComponent);
        }

        newEnemy.transform.parent = transform;

        //spawning sprite object of the enemy
        //newEnemyComponent.InitSprite(enemyToSpawn.animatedPrefab);

    }

    private void SpawnBossEnemy(Enemy newBoss)
    {
        if(bossEnemiesList == null) { bossEnemiesList = new List<Enemy>(); }

        bossEnemiesList.Add(newBoss);

        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }
}
