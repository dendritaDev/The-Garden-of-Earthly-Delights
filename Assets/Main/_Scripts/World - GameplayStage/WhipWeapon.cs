using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{
    [SerializeField] GameObject whipObject;
    [SerializeField] GameObject whipObject2;
    
    PlayerMove playerMove;
    [SerializeField] Vector2 attackSize = new Vector2(3f, 2f);
    [SerializeField] private AudioSource attackSound;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        attackSound = GetComponent<AudioSource>();
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
            Collider2D[] colliders = Physics2D.OverlapBoxAll(whipObject.transform.position, attackSize, 0f, layerMask);
            ApplyDamage(colliders);
            Collider2D[] colliders2 = Physics2D.OverlapBoxAll(whipObject2.transform.position, attackSize, 180f, layerMask);
            ApplyDamage(colliders2);

            attackSound.Play();
            yield return new WaitForSeconds(0.3f);
        }
     
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.Draw
        //Gizmos.DrawLine(transform.position, transform.position + displacement.normalized);
    }
}
