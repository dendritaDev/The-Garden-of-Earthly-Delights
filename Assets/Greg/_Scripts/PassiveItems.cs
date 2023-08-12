using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;
    Character character;

    

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        
    }

    public void Equip(Item itemToEquip)
    {

        if(items == null)
            items = new List<Item>();

        Item newItemIstance = new Item();
        newItemIstance.Init(itemToEquip.Name);
        newItemIstance.stats.Sum(itemToEquip.stats);

        items.Add(newItemIstance);
        newItemIstance.Equip(character);
    }

    public void Unequip(Item itemToUnequip)
    {
        if (items == null)
            items = new List<Item>();

        Item newItemIstance = new Item();
        newItemIstance.Unequip(character);
    }

    internal void UpgradeItem(UpgradeData upgradeData)
    {
        Item itemToUpgrade = items.Find(id => id.Name == upgradeData.item.Name);
        itemToUpgrade.Unequip(character);
        itemToUpgrade.stats.Sum(upgradeData.itemStats);
        itemToUpgrade.Equip(character);
    }
}
