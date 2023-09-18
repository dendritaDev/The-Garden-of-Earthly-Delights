using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;

    [SerializeField] PoolManager poolManager;

    [SerializeField]
    WeaponData startingWeapon;

    List<WeaponBase> weapons;

    Character character;

    private void Awake()
    {
        weapons = new List<WeaponBase>();
        character = GetComponent<Character>();
    }

    private void Start()
    {

        AddWeapon(startingWeapon);
    }
    public void AddWeapon(WeaponData weaponData)
    {
        
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);

        //si ya tenemos esa arma la rotamos un poco para que no este en el mismo sitio
        if(CheckForRepeatedWeapon(weaponData))
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(40, 90)));
            weaponGameObject.transform.rotation = rot;
        }

        WeaponBase weaponBase = weaponGameObject.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weaponBase.SetPoolManager(poolManager);
        weapons.Add(weaponBase);
        weaponBase.AddOwnerCharacter(character);

        Level level = GetComponent<Level>();
        if(level != null)
        {
            level.AddUpgradesIntoTheListOfAvailableUpgrades(weaponData.upgrades);
        }
    }

    public bool CheckForRepeatedWeapon(WeaponData weaponData)
    {
        int numWeapons = weapons.Count;

        for (int i = 0; i < numWeapons; i++)
        {
            if (weapons[i].weaponData.Name == weaponData.Name)
            {
                return true;
            }
        }

        return false;
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);
    }
}
