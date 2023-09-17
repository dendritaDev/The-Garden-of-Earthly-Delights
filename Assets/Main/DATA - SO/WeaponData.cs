using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public int damage;
    public float timeToAttack;
    public int numberOfAttacks;
    public int numOfPerforationHits;
    public float projectileSpeed;
    public float detectionRadius;
    public float stun;
    public float knockback;
    public float knockbackTimeWeight;
    public float areaSize;

    public WeaponStats(WeaponStats stats)
    {
        this.damage = stats.damage;
        this.timeToAttack = stats.timeToAttack;
        this.numberOfAttacks = stats.numberOfAttacks;
        this.numOfPerforationHits = stats.numOfPerforationHits;
        this.projectileSpeed = stats.projectileSpeed;
        this.detectionRadius = stats.detectionRadius;
        this.stun = stats.stun;
        this.knockback = stats.knockback;
        this.knockbackTimeWeight = stats.knockbackTimeWeight;
        this.areaSize = stats.areaSize;
    }

    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
        this.numOfPerforationHits += weaponUpgradeStats.numOfPerforationHits;
        this.projectileSpeed += weaponUpgradeStats.projectileSpeed;
        this.detectionRadius += weaponUpgradeStats.detectionRadius;
        this.stun += weaponUpgradeStats.stun;
        this.knockback += weaponUpgradeStats.knockback;
        this.knockbackTimeWeight += weaponUpgradeStats.knockbackTimeWeight;
        this.areaSize += weaponUpgradeStats.areaSize;
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{ 
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}
