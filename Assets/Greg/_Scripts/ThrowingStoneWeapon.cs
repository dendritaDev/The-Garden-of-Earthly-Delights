using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingStoneWeapon : WeaponBase
{
    [SerializeField] GameObject stonePrefab;

    //[SerializeField] float spread = 0.5f;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Attack()
    {
        

        for(int i = 0; i <weaponStats.numberOfAttacks; i++)
        {
            //SpawnProjectile(stonePrefab, transform.position);

            //Si quisiera que apareciesen uno encima del otro:

            //{
            //    Vector3 newStonePosition = transform.position;
            //    if (weaponStats.numberOfAttacks > 1)
            //    {
            //        newStonePosition.y -= (spread * (weaponStats.numberOfAttacks - 1)) / 2;
            //        newStonePosition.y += i * spread;
            //    }

            //    thrownStone.transform.position = newStonePosition;
            //}


        }


    }
}
