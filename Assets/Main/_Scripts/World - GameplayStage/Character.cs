using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    public int currentHP = 100;
    public int armor = 0;
    public float hpRegenerationRate = 1f;
    [SerializeField] private int hpRegenerationAmount = 1;
    [HideInInspector] public float hpRegenerationTimer;
    [SerializeField] private float damageBonus;
    [SerializeField] private float critChance;

    public float Speed { get => playerMove.speed; set { playerMove.speed += value; } }
    public int MaxHP { get => maxHP; set { maxHP += value; } }
    public float DamageBonus { get => damageBonus; set => damageBonus += value; }
    public int HpRegenerationAmount { get => hpRegenerationAmount; set { hpRegenerationAmount += value; } }
    public float CritChance { get => critChance; set { if (critChance < 100) { critChance = value; } } }

    [SerializeField] StatusBar hpBar;
    [SerializeField] SpriteRenderer body;

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
        DamageBonus = 1f + 2.5f * damageUpgradeLevel; //Trabajaremos con porcentajes, 100% del daño, 105%...

        int speedUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Speed);
        playerMove.speedBonus = speedUpgradeLevel;

        int hpRegenerationUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HPRegeneration);
        HpRegenerationAmount = 1 + hpRegenerationUpgradeLevel;

        int critChanceUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.CritChance);
        CritChance += (critChanceUpgradeLevel * 10);


    }

    private void Update()
    {

        if (pauseManager.isGamePaused)
            return;

        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;

        if(hpRegenerationTimer > 1f)
        {
            Heal(HpRegenerationAmount);
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

        StartCoroutine(GameFeelTakingDamage());

    }

    public IEnumerator GameFeelTakingDamage()
    {
        body.color = Color.red;
        //play music

        yield return new WaitForSeconds(0.1f);

        body.color = Color.white;
        
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

