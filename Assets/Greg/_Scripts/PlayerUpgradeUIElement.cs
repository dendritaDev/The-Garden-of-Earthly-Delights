using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersistentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;

    [SerializeField] DataContainer dataContainer;

    private void Start()
    {
        UpdateElement();
    }

    public void Upgrade()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade]; //el panel tendra un upgrade de playerpersistenupgrades determinado y eso lo buscara en el datacontainer para obtener lo demás

        if(playerUpgrades.level >= playerUpgrades.max_level) { return; }

        if(dataContainer.coins >= playerUpgrades.costToUpgrade)
        {
            dataContainer.coins -= playerUpgrades.costToUpgrade;
            playerUpgrades.level += 1;
            UpdateElement();
        }
    }
    void UpdateElement()
    {
        PlayerUpgrades playerUpgrade = dataContainer.upgrades[(int)upgrade];
        upgradeName.text = upgrade.ToString();
        level.text = playerUpgrade.level.ToString();
        price.text = playerUpgrade.costToUpgrade.ToString();
    }
}
