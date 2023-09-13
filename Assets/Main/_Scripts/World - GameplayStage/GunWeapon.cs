using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunWeapon : WeaponBase
{
    //esto se orientará solo apuntando a los enemigos
    //[SerializeField] GameObject gunPrefab;

    //esto seran los proyectiles que se dispararán
    [SerializeField] PoolObjectData bulletPrefab;

    private List<Vector3> enemyPos = new List<Vector3>();
    private Vector3 selectedOne = Vector3.zero;

    public override void Attack()
    {
        enemyPos.Clear();
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponStats.detectionRadius, layerMask);

            if(colliders.Length == 0) { return; }
            float nearEnemy = Mathf.Infinity;
            float distanceToEnemy;
            int enemyIndex = -1;

            for (int e = 0; e < colliders.Length; e++)
            {

                if (!colliders[e].CompareTag("Enemy")) //solo nos interesan los colliders de enemigos
                {
                    Debug.Log(colliders[e].tag);
                    Debug.Log(colliders[e].name);
                    continue;

                }


                distanceToEnemy = Vector3.Distance(this.transform.position, colliders[e].transform.position);

                enemyPos.Add(colliders[e].transform.position);

                if (distanceToEnemy < nearEnemy)
                {
                    nearEnemy = distanceToEnemy;
                    enemyIndex = e;
                }
            }

            if(enemyIndex == -1) { return; } //si no hay ningun enemigo return
            selectedOne = colliders[enemyIndex].transform.position;
            GameObject bullet = SpawnProjectile(bulletPrefab, transform.position, colliders[enemyIndex].transform.position);


            Vector3 direction = colliders[enemyIndex].transform.position - transform.position;
            float angleRadians = Mathf.Atan2(-direction.y, -direction.x);
            float angleDegree = angleRadians * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angleDegree, Vector3.forward);
            

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, weaponStats.detectionRadius);

        if(enemyPos.Count > 1)
        {
            foreach (var enemy in enemyPos)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(this.transform.position, enemy);
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, selectedOne);
    }

}
