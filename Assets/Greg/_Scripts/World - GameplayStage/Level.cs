using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> randomUpgradesList;
    [SerializeField]List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;
    PassiveItems passiveItems;

    [SerializeField]List<UpgradeData> upgradesAvailableOnStart;

    int TO_LEVEL_UP
    {
        get { return level * 1000; }
    }

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
    }

    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvailableUpgrades(upgradesAvailableOnStart);
    }

    internal void AddUpgradesIntoTheListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null) { return; }
        this.upgrades.AddRange(upgradesToAdd);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    internal void Upgrade(int selectedUpgrade)
    {
        UpgradeData upgradeData = randomUpgradesList[selectedUpgrade];

        if(acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                passiveItems.UpgradeItem(upgradeData);
                break;
            case UpgradeType.WeaponGet:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemGet:
                passiveItems.Equip(upgradeData.item);
                AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.item.upgrades);
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public void CheckLevelUp()
    {
        if(experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (randomUpgradesList == null) { randomUpgradesList = new List<UpgradeData>(); }
        randomUpgradesList.Clear();
        randomUpgradesList.AddRange(GetUpgrades(4));
            
        upgradePanelManager.OpenPanel(randomUpgradesList);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    /// <summary>
    /// Mezclar de posicion los upgrades para que tengan diferente orden
    /// </summary>
    public void ShuffleUpgrades()
    {
        for (int i = upgrades.Count - 1; i > 0; i--)
        {
            int x = Random.Range(0, i + 1);

            UpgradeData shufleElement = upgrades[i];
            upgrades[i] = upgrades[x];
            upgrades[x] = shufleElement;

        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades();
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        
        if(count > upgrades.Count)
        {
            count = upgrades.Count;
        }
        
        for(int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[i]);
        }
        

        return upgradeList;

    }
}
