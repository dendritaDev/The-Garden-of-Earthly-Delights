using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunWeapon : WeaponBase
{
    //esto se orientará solo apuntando a los enemigos
    //[SerializeField] GameObject gunPrefab;

    //esto seran los proyectiles que se dispararán
    [SerializeField] PoolObjectData bulletPrefab;


    public override void Attack()
    {
        
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponStats.detectionRadius);

            if(colliders.Length == 0) { return; }
            float nearEnemy = weaponStats.detectionRadius + 1; //este numero siempre será mayor q cualquier enemigo detectado
            float distanceToEnemy;
            int enemyIndex = -1;

            for (int e = 0; e < colliders.Length; e++)
            {

                if (!colliders[e].CompareTag("Enemy")) //solo nos interesan los colliders de enemigos
                    continue;

                Debug.Log("b");

                distanceToEnemy = Vector3.Distance(this.transform.position, colliders[e].transform.position);
                if (distanceToEnemy < nearEnemy)
                {
                    enemyIndex = e;
                }
            }

            if(enemyIndex == -1) { return; } //si no hay ningun enemigo return

            GameObject bullet = SpawnProjectile(bulletPrefab, transform.position, colliders[enemyIndex].transform.position);
            Debug.Log(colliders[enemyIndex].transform.position - transform.position);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, weaponStats.detectionRadius);
    }

}
