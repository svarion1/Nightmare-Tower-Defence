using UnityEngine;
using System.Collections;

public class TorrettaVelenosa : Turret
{

   public float tempoDurataVeleno, tempoAttivazione, dannoVeleno;

   protected override void Shoot(Transform target)
   {
      projectile.GetComponent<ProiettileVelenoso>().danno = damage;
      projectile.GetComponent<ProiettileVelenoso>().tempoDurataVeleno = tempoDurataVeleno;
      projectile.GetComponent<ProiettileVelenoso>().tempoAttivazione = tempoAttivazione;
      projectile.GetComponent<ProiettileVelenoso>().dannoVeleno = dannoVeleno;
      base.Shoot(target);
   }
}
