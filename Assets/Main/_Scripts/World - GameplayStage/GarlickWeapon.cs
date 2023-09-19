using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlickWeapon : WeaponBase
{

    
    [SerializeField] ParticleSystem slash;
    [SerializeField] ParticleSystem backgroundSlash;
    [SerializeField] ParticleSystem particles;
    [SerializeField] private AudioSource attackSound;

    private void Awake()
    {
        attackSound = GetComponent<AudioSource>();

    }

    //Actaualizamos el radio de los sistemas de particulas a el radio de la colision
    public float AttackAreaSize 
    { 
        get => weaponStats.areaSize; 
        set 
        {
            weaponStats.areaSize = value;
            slash.startSize = (weaponStats.areaSize / 2);
            backgroundSlash.startSize = (weaponStats.areaSize / 2);
            
            ParticleSystem.ShapeModule ps = particles.GetComponent<ParticleSystem>().shape;
            ps.radius = weaponStats.areaSize;
               
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
            attackSound.Play();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponStats.areaSize, layerMask);
            ApplyDamage(colliders);
            yield return new WaitForSeconds(0.15f);

        }

    }



}
