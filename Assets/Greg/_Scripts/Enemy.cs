using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorSteering;

[Serializable]
public class EnemyStats
{
    public int hp = 4;
    public int damage = 1;
    public int experienceReward = 400;
    public float moveSpeed = 1.0f;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experienceReward = stats.experienceReward;
        this.moveSpeed = stats.moveSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
    }
}
public class Enemy : MonoBehaviour, IDamageable, IPoolMember
{
    [SerializeField] Transform targetDestination;
    GameObject targetGameObject;
    Character targetCharacter;
    

    Rigidbody2D _rigidbody2D;

    public EnemyStats stats;

    [SerializeField] EnemyData enemyData;

    //float stunned;
    Vector3 knocnkbackVector;
    float knockbackForce; //a mas fuerza, mayor distancia de knockback
    float knockbackTimeWeight; //a mas tiempo, mas duracion del knockback

    public PoolMember poolMember;

    private AgentBehavior[] behaviors;
    private Agent agent;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        behaviors = GetComponents<AgentBehavior>();
        agent = GetComponent<Agent>();


    }

    private void Start()
    {
        /*if(enemyData != null)
        {
            InitSprite(enemyData.animatedPrefab);

            //aunque esto ya lo hacemos en el SpawnEnemy de Enemis Manager:
            SetStats(enemyData.stats);
            SetTarget(GameManager.instance.playerTransform.gameObject);
            //Lo hago aqui de nuevo tambien por si quiero testear con enemigos y no me quiero esperar a que spawneen o algo
            //que arrastrandolos simplemente en el juego puedan funcionar
        }*/

    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;

        foreach (var behavior in behaviors)
        {
            behavior.SetTarget(target);
        }
    }

    private void FixedUpdate()
    {

        //ProcessStun();
        //Ahora se mueven a partir de Behavior Steering
        //Move();
    }

    //private void ProcessStun()
    //{
    //    if(stunned > 0f)
    //    {
    //        _rigidbody2D.velocity = Vector2.zero;
    //        stunned -= Time.fixedDeltaTime; //fixeddeltatime porque estamos en feixed update
    //    }
        
    //}

    //private void ProcessStunForAgent()
    //{
    //    if (stunned > 0f)
    //    {
    //        agent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //        stunned -= Time.fixedDeltaTime; //fixeddeltatime porque estamos en feixed update
    //    }

    //}

    //private void Move()
    //{
    //    Vector3 direction = (targetDestination.position - transform.position).normalized;
    //    _rigidbody2D.velocity = CalculateMovementVelocity(direction) + CalculateKnockback();
    //}

    //private Vector3 CalculateMovementVelocity(Vector3 direction)
    //{
    //    return direction * stats.moveSpeed * (stunned > 0f ? 0f : 1f); //si esta stuneado la velocidad de movimiento es 0 y solo se movera si es knockbackeado
    //}

    private Vector3 CalculateKnockback()
    {
        if (knockbackTimeWeight > 0f)
        {
            knockbackTimeWeight -= Time.fixedDeltaTime;
        }
        return knocnkbackVector * knockbackForce * (knockbackTimeWeight > 0f ? 1f : 0f); //mientras time no sea 0, seguimos haciendo knockback
    }

    public void Knockback(Vector3 direction, float force, float timeWeight)
    {
        knocnkbackVector = direction;
        knockbackForce = force;
        knockbackTimeWeight = timeWeight;
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }
            targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage)
    {
        stats.hp -= damage;

        if(stats.hp < 1)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        targetGameObject.GetComponent<Level>().AddExperience(stats.experienceReward);
        GetComponent<DropOnDestroy>().CheckDrop();

        if(poolMember != null)
        {
            poolMember.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    //public void InitSprite(GameObject animatedPrefab)
    //{
    //    GameObject spriteObject = Instantiate(animatedPrefab);
    //    spriteObject.transform.parent = transform;
    //    spriteObject.transform.localPosition = Vector3.zero;
    //}

    public void Stun(float stun)
    {
        agent.stunned = stun; 
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;

    }
}
