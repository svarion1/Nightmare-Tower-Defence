using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageTurret : Turret
{
    protected override void NextAttackUpdate()
    {
        if (nextAttackTime > 0)
        {
            nextAttackTime -= Time.deltaTime;
        }
        else if (nextAttackTime <= 0)
        {
            FindEnemies();
         
            if (enemyColliders.Length > 0)
            {
                // Shoots to the every enemy in range then reset the attack time
                Shoot();
                nextAttackTime = initialTime;
            }
        }
    }

    protected void Shoot()
    {
        foreach (var c in enemyColliders)
        {
            c.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
