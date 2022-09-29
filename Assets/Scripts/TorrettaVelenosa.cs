using UnityEngine;
using System.Collections;

public class TorrettaVelenosa : Turret
{

   public float tempoDurataVeleno, tempoAttivazione, dannoVeleno;

   protected override void Spara(Transform target)
   {
      proiettile.GetComponent<ProiettileVelenoso>().danno = damage;
      proiettile.GetComponent<ProiettileVelenoso>().tempoDurataVeleno = tempoDurataVeleno;
      proiettile.GetComponent<ProiettileVelenoso>().tempoAttivazione = tempoAttivazione;
      proiettile.GetComponent<ProiettileVelenoso>().dannoVeleno = dannoVeleno;
      base.Spara(target);
   }
}
