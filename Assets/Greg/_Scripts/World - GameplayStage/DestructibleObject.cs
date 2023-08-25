using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageable
{
    public void Knockback(Vector3 direction, float force, float timeWeight)
    {
        
    }

    public void Stun(float stun)
    {
       
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
        GetComponent<DropOnDestroy>().CheckDrop();
    }
}
