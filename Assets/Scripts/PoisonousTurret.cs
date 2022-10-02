using UnityEngine;
using System.Collections;

public class PoisonousTurret : Turret
{

   public float poisonDuration, poisonEffectFrequency, poisonDamage;

   protected override void Shoot(Transform target)
   {
      projectile.GetComponent<ProjectilePoisonous>().damage = damage;
      projectile.GetComponent<ProjectilePoisonous>().poisonDuration = poisonDuration;
      projectile.GetComponent<ProjectilePoisonous>().poisonEffectFrequency = poisonEffectFrequency;
      projectile.GetComponent<ProjectilePoisonous>().poisonDamage = poisonDamage;
      base.Shoot(target);
   }
}
