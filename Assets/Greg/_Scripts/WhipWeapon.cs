using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{
    [SerializeField] GameObject whipObject;
    
    PlayerMove playerMove;
    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    public override void Attack()
    {

        StartCoroutine(MultipleAttackProcess());
    }

    IEnumerator MultipleAttackProcess()
    {
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            whipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(whipObject.transform.position, attackSize, 0f);
            ApplyDamage(colliders);

            yield return new WaitForSeconds(0.3f);
        }
     
    }

  
}
