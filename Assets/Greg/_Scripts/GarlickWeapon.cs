using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlickWeapon : WeaponBase
{

    [SerializeField] float attackAreaSize = 3f;


    public override void Attack()
    {
        StartCoroutine(MultipleAttackProcess());
    }

    

    IEnumerator MultipleAttackProcess()
    {

        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackAreaSize);
            ApplyDamage(colliders);
            yield return new WaitForSeconds(0.15f);

        }

    }



}
