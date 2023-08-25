using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;
    public int armor = 0;
    //MORE STATS TO ADD
    

    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;

    public float damageBonus;

    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private bool isDead = false;

    [SerializeField] DataContainer dataContainer;
    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        ApplyPersistentUpgrades();
        hpBar.SetState(currentHP, maxHP);
    }

    private void ApplyPersistentUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HP);
        maxHP += maxHP / 10 * hpUpgradeLevel;
        currentHP = maxHP;

        int damageUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Damage);
        damageBonus = 1f + 0.1f * damageUpgradeLevel; //Trabajaremos con porcentajes, 100% del daño, 105%...
    }

    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;

        if(hpRegenerationTimer > 1f)
        {
            Heal(1);
            hpRegenerationTimer -= 1f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead == true) { return; }

        ApplyArmor(ref damage);

        MessageSystem.instance.DamagePlayerPopup(damage.ToString(), this.transform.position);

        currentHP -= damage;

        if (currentHP < 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }

        hpBar.SetState(currentHP, maxHP);
    }

    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if (damage < 0)
            damage = 0;
    }

    public void Heal(int amount, bool displayPopup = false)
    {
        if(currentHP <= 0) { return; }

        currentHP += amount;

        if(currentHP > maxHP) { currentHP = maxHP; }

        hpBar.SetState(currentHP, maxHP);

        if(displayPopup)
            MessageSystem.instance.HealPopup(amount.ToString(), this.transform.position, false);

    }

}

