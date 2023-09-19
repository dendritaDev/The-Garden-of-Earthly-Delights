    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolMember
{
    public PoolMember poolMember;
    WeaponBase weapon;

    float speed;
    int damage = 5;
    [SerializeField] int numOfPerforationHits = 3;
    [SerializeField] int numOfPerforationHitsReset;

    [SerializeField] float hitArea = 1f;
    float timeToDestroy = 1.5f;

    List<IDamageable> alreadyHitTargets;

    [SerializeField]
    [Tooltip("Cada cuantos frames se llama al update. Solo tocara para optimizacion. Para modificar velocidad -> cambiar speed")]
    int frameRate = 2;

    [HideInInspector] public Vector3 directionToMove;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePB();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        LifeTimer();
    }

    private void LifeTimer()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy < 0)
        {
            DestroyProjectile();
        }
    }

    private void MovePB()
    {
        directionToMove.z = 0;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        rb.velocity = speed * directionToMove;
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numOfPerforationHits > 0)
        {
            IDamageable objectHit = collision.GetComponent<IDamageable>();
            if (objectHit != null)
            {
                if (!CheckRepeatHit(objectHit))
                {

                    weapon.ApplyDamage(damage, collision.transform.position, objectHit);
                    alreadyHitTargets.Add(objectHit);
                    numOfPerforationHits--;
                }
            }
        }
        else
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if(poolMember == null)
        {
            Destroy(gameObject);
        }
        else
        {
            poolMember.ReturnToPool();
        }
        
    }


    private bool CheckRepeatHit(IDamageable objectHit)
    {
        if (alreadyHitTargets == null) { alreadyHitTargets = new List<IDamageable>(); }

        return alreadyHitTargets.Contains(objectHit);
    }

   

    internal void SetStats(WeaponBase weaponBase)
    {
        weapon = weaponBase;
        damage = weaponBase.GetDamageStat();
        numOfPerforationHits = weaponBase.weaponStats.numOfPerforationHits;
        numOfPerforationHitsReset = weaponBase.weaponStats.numOfPerforationHits;
        speed = weaponBase.weaponStats.projectileSpeed;
        
    }

    private void OnEnable()
    {
        timeToDestroy = 3f;
        numOfPerforationHits = numOfPerforationHitsReset;

    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;

        if(alreadyHitTargets != null)
        {
            for (int i = 0; i < alreadyHitTargets.Count; i++)
                alreadyHitTargets.RemoveAt(i);
        }
     
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }

}
