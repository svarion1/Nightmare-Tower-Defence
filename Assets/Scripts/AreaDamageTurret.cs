using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageTurret : Turret
{
    protected override void FindEnemies()
    {
        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, attackRange, hitColliders, enemiesMask);

        for (int i = 0; i < numColliders; i++)
        {
            hitColliders[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
