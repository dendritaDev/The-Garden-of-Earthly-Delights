using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DirectionOfAttack
{
    None,
    Forward,
    LeftRight,
    UpDown
}
public abstract class WeaponBase : MonoBehaviour
{
    public LayerMask layerMask;
   
    public WeaponData weaponData;
    public WeaponStats weaponStats;

    float timer;

    Character wielder;
    public Vector2 vectorOfAttack;
    [SerializeField] DirectionOfAttack attackDirection;

    private PauseManager pauseManager;

    public void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    PoolManager poolManager;
    public void Update()
    {
        if (pauseManager.isGamePaused)
            return;

        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
            
        }
    }

    public void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamageStat();

        foreach (var collider in colliders)
        {
            IDamageable e = collider.GetComponent<IDamageable>();
            if (e != null)
            {
                ApplyDamage(damage, collider.transform.position, e);
            }

        }
    }

    public void ApplyDamage(int damage, Vector3 position, IDamageable e)
    {
        bool isCritical = false;

        if (UnityEngine.Random.Range(0f, 100f) < wielder.CritChance)
        {
            isCritical = true;
            damage *= 2;
        }



        PostDamage(damage, position, isCritical);
        e.TakeDamage(damage);
        ApplyAdditionalEffects(e, position);
    }

    private void ApplyAdditionalEffects(IDamageable e, Vector3  enemyPosition)
    {
        e.Stun(weaponStats.stun);
        e.Knockback((enemyPosition - transform.position).normalized, weaponStats.knockback, weaponStats.knockbackTimeWeight);
    }

    public virtual void SetData(WeaponData data)
    {
        weaponData = data;

        weaponStats = new WeaponStats(data.stats);
    }

    public void SetPoolManager(PoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    public abstract void Attack();

    public int GetDamageStat()
    {
        int damage = (int)(weaponData.stats.damage * wielder.DamageBonus);
        return damage;
    }
    public virtual void PostDamage(int damage, Vector3 targetPosition, bool isCritical)
    {
        MessageSystem.instance.DamagePopup(damage.ToString(), targetPosition, isCritical);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    internal void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }


    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector3 projectilePos, Vector3 enemyPosition)
    {
        GameObject projectileGO = poolManager.GetObject(poolObjectData);

        projectileGO.transform.position = projectilePos;


        Projectile projectileComponent = projectileGO.GetComponent<Projectile>();
        projectileComponent.directionToMove = enemyPosition - projectilePos;
        projectileComponent.SetStats(this);

        return projectileGO;

    }

}