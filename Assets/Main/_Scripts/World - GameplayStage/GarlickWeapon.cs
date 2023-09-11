using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlickWeapon : WeaponBase
{

    [SerializeField] float attackAreaSize = 3f;
    [SerializeField] ParticleSystem slash;
    [SerializeField] ParticleSystem backgroundSlash;
    [SerializeField] ParticleSystem particles;

    //Actaualizamos el radio de los sistemas de particulas a el radio de la colision
    public float AttackAreaSize 
        { get => attackAreaSize; 
        
        set { 
            attackAreaSize = value;
            slash.startSize = (attackAreaSize / 2);
            backgroundSlash.startSize = (attackAreaSize / 2);
            
            ParticleSystem.ShapeModule ps = particles.GetComponent<ParticleSystem>().shape;
            ps.radius = attackAreaSize;
               
            }  
        }


    public override void Attack()
    {
        StartCoroutine(MultipleAttackProcess());
    }

    

    IEnumerator MultipleAttackProcess()
    {

        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            slash.Play();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackAreaSize, layerMask);
            ApplyDamage(colliders);
            yield return new WaitForSeconds(0.15f);

        }

    }



}
