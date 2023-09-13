using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 100;
    public int currentHP = 100;
    public int armor = 0;
    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;
    [SerializeField]
    private float damageBonus;
    public float critChance;

    public float Speed { get => playerMove.speed; set { playerMove.speed += value; } }
    public int MaxHP { get => maxHP; set { maxHP += value; } }
    public float DamageBonus { get => damageBonus; set => damageBonus += value; }

    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private bool isDead = false;

    [SerializeField] DataContainer dataContainer;

    private PauseManager pauseManager;

    private PlayerMove playerMove;
    

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        ApplyPersistentUpgrades();
        hpBar.SetState(currentHP, MaxHP);
        pauseManager = FindObjectOfType<PauseManager>();
        
    }

    private void ApplyPersistentUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HP);
        MaxHP += MaxHP / 50 * hpUpgradeLevel;
        currentHP = MaxHP;

        int damageUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Damage);
        DamageBonus = 1f + 0.1f * damageUpgradeLevel; //Trabajaremos con porcentajes, 100% del daño, 105%...
    }

    private void Update()
    {

        if (pauseManager.isGamePaused)
            return;

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

        hpBar.SetState(currentHP, MaxHP);
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

        if(currentHP > MaxHP) { currentHP = MaxHP; }

        hpBar.SetState(currentHP, MaxHP);

        if(displayPopup)
            MessageSystem.instance.HealPopup(amount.ToString(), this.transform.position, false);

    }

}

