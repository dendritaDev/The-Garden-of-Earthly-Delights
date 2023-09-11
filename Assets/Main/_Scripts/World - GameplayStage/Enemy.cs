using System;
using System.Collections;
using UnityEngine;

using BehaviorSteering;
using DG.Tweening;
using System.Collections.Generic;

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
    //Vector3 knocnkbackVector;
    //float knockbackForce; //a mas fuerza, mayor distancia de knockback
    //float knockbackTimeWeight; //a mas tiempo, mas duracion del knockback

    public PoolMember poolMember;

    private AgentBehavior[] behaviors;
    private Agent agent;

    public Sprite splashSprite;
    public ParticleSystem splashExplosion;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        behaviors = GetComponents<AgentBehavior>();
        agent = GetComponent<Agent>();


    }

    private void Start()
    {
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

    }

 
    public void Knockback(Vector3 direction, float force, float timeWeight)
    {
        agent.knocnkbackVector = direction;
        agent.knockbackForce = force;
        agent.knockbackTimeWeight = timeWeight;
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

        StartCoroutine(DestroyEnemy());

    }

    public IEnumerator DestroyEnemy()
    {
        var splashFX = Instantiate(splashExplosion, transform.position, Quaternion.identity, gameObject.transform);

        Color colorFromSplashExplosion = splashFX.main.startColor.gradient.Evaluate(UnityEngine.Random.Range(0f, 1f));
        splashFX.startColor = colorFromSplashExplosion;

        yield return new WaitForEndOfFrame();
        
        GameObject GO = new GameObject();
        GO.transform.position = transform.position;
        SpriteRenderer spriteRenderer = GO.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = splashSprite;
        spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        GO.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        yield return new WaitForEndOfFrame();

        GameObject paintingCanvas = GameObject.FindGameObjectWithTag("Painting");

        yield return new WaitForSeconds(0.15f);

        var sequence = DOTween.Sequence()
           .Append(spriteRenderer.DOColor(colorFromSplashExplosion, 0.5f))
           .Join(spriteRenderer.DOFade(UnityEngine.Random.Range(0.15f, 0.7f), 0.5f))
           .Join(spriteRenderer.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f))
           .OnComplete(() =>
           {
               spriteRenderer.sortingOrder = -7;
           });

        yield return new WaitForEndOfFrame();

        GO.transform.parent = paintingCanvas.transform;


        if (poolMember != null)
        {
            poolMember.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Stun(float stun)
    {
        agent.stunned = stun; 
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;

    }
}
